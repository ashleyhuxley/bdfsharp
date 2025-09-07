using System.Buffers;
using System.Drawing;
using System.IO.Pipelines;
using System.Text;

namespace ElectricFox.BdfFontLib
{
    internal class BdfFontLoader : BdfLoader
    {
        private BdfCharacterLoader? currentChar = null;

        private bool isReadingProperties;

        private string? _fontName;
        private string? _version;
        private BdfSize? _size;
        private BdfBoundingBox? _fontBoundingBox;
        private int _charCount;
        private readonly Dictionary<string, string> _properties = new();
        private List<BdfGlyph> _chars = new();

        public async Task<BdfFont> LoadAsync(string filePath)
        {
            var pipe = new Pipe();
            PipeReader reader = pipe.Reader;
            PipeWriter writer = pipe.Writer;

            using (var file = File.OpenRead(filePath))
            {
                Task writing = FillPipeAsync(file, writer);
                Task reading = ReadPipeAsync(reader);
                await Task.WhenAll(reading, writing);
            }

            var geommetry = new BdfGeometry
            {
                ScalableWidth = _SWidth,
                DeviceWidth = _DWidth,
                ScalableWidth1 = _SWidth1,
                DeviceWidth1 = _DWidth1,
                VVector = _VVector,
            };

            return new BdfFont
            {
                FontName = _fontName ?? throw new BdfLoadException("Font name was not specified"),
                Version = _version ?? string.Empty,
                Size = _size ?? throw new BdfLoadException("SIZE was not specified"),
                FontBoundingBox = _fontBoundingBox ?? throw new BdfLoadException("Bounding Box was not specified"),
                CharCount = _charCount,
                Properties = new Dictionary<string, string>(_properties),
                Chars = _chars.ToDictionary(c => c.Encoding),
                Geometry = geommetry
            };
        }

        private async Task FillPipeAsync(FileStream stream, PipeWriter writer)
        {
            const int minimumBufferSize = 512;

            while (true)
            {
                // Allocate at least 512 bytes from the PipeWriter.
                Memory<byte> memory = writer.GetMemory(minimumBufferSize);
                try
                {
                    int bytesRead = await stream.ReadAsync(memory);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    // Tell the PipeWriter how much was read from the Socket.
                    writer.Advance(bytesRead);
                }
                catch (Exception ex)
                {
                    //LogError(ex);
                    break;
                }

                // Make the data available to the PipeReader.
                FlushResult result = await writer.FlushAsync();

                if (result.IsCompleted)
                {
                    break;
                }
            }

            // By completing PipeWriter, tell the PipeReader that there's no more data coming.
            await writer.CompleteAsync();
        }

        private async Task ReadPipeAsync(PipeReader reader)
        {
            while (true)
            {
                ReadResult result = await reader.ReadAsync();
                ReadOnlySequence<byte> buffer = result.Buffer;

                while (TryReadLine(ref buffer, out ReadOnlySequence<byte> line))
                {
                    // Process the line.
                    ProcessLine(line);
                }

                // Tell the PipeReader how much of the buffer has been consumed.
                reader.AdvanceTo(buffer.Start, buffer.End);

                // Stop reading if there's no more data coming.
                if (result.IsCompleted)
                {
                    break;
                }
            }

            // Mark the PipeReader as complete.
            await reader.CompleteAsync();
        }

        private bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
        {
            // Look for LF
            SequencePosition? position = buffer.PositionOf((byte)'\n');

            if (position == null)
            {
                line = default;
                return false;
            }

            // Slice up to the LF (not including it)
            line = buffer.Slice(0, position.Value);

            // If the line ends with CR, drop it
            if (!line.IsEmpty)
            {
                var lastByte = line.Slice(line.Length - 1, 1);
                if (lastByte.FirstSpan[0] == (byte)'\r')
                {
                    line = line.Slice(0, line.Length - 1);
                }
            }

            // Advance buffer past LF
            buffer = buffer.Slice(buffer.GetPosition(1, position.Value));

            return true;
        }

        public static (string Keyword, string Value) ParseAttribute(string line)
        {
            int spaceIndex = line.IndexOf(' ');
            if (spaceIndex < 0)
            {
                throw new FormatException("Line does not contain a space separating keyword and value.");
            }

            string keyword = line.Substring(0, spaceIndex).Trim();
            string rawValue = line.Substring(spaceIndex + 1).Trim();

            string value = rawValue.Trim();
            if (value.StartsWith("\"") && value.EndsWith("\"") && value.Length >= 2)
            {
                value = value.Substring(1, value.Length - 2);
            }

            return (keyword, value);
        }

        private void ProcessLine(ReadOnlySequence<byte> line)
        {
            // Convert the line to a string and process it.
            var lineString = Encoding.UTF8.GetString(line.ToArray());

            if (currentChar is not null)
            {
                var complete = currentChar.ParseCharacter(lineString);
                if (complete)
                {
                    _chars.Add(currentChar.GetCharacter());
                    currentChar = null;
                }
                return;
            }

            if (isReadingProperties)
            {
                if (string.Equals(lineString, "ENDPROPERTIES", StringComparison.OrdinalIgnoreCase))
                {
                    isReadingProperties = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(lineString))
                {
                    return;
                }

                var property = ParseAttribute(lineString);
                _properties[property.Keyword] = property.Value;
            }
            else
            {
                var values = lineString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 0)
                {
                    return;
                }

                var command = values[0].ToUpperInvariant();
                var attributeCount = values.Length - 1;

                switch (command)
                {
                    case "STARTFONT":
                        CheckAttributeLength(1, attributeCount, command);
                        _version = values[1];
                        break;
                    case "STARTCHAR":
                        CheckAttributeLength(1, attributeCount, command);
                        currentChar = new BdfCharacterLoader(values[1]);
                        break;
                    case "FONT":
                        CheckAttributeLength(1, attributeCount, command);
                        _fontName = values[1];
                        break;
                    case "SIZE":
                        CheckAttributeLength(3, attributeCount, command);
                        _size = BdfSize.Parse(values[1], values[2], values[3]);
                        break;
                    case "FONTBOUNDINGBOX":
                        CheckAttributeLength(4, attributeCount, command);
                        _fontBoundingBox = BdfBoundingBox.Parse(values[1], values[2], values[3], values[4]);
                        break;
                    case "CHARS":
                        CheckAttributeLength(1, attributeCount, command);
                        _charCount = int.Parse(values[1]);
                        break;
                    case "STARTPROPERTIES":
                        CheckAttributeLength(1, attributeCount, command);
                        isReadingProperties = true;
                        _properties.Clear();
                        break;
                    case "ENDPROPERTIES":
                        CheckAttributeLength(0, attributeCount, command);
                        isReadingProperties = false;
                        break;
                    case "SWIDTH":
                        CheckAttributeLength(2, attributeCount, command);
                        _SWidth = ParseSize(values[1], values[2], command);
                        break;
                    case "DWIDTH":
                        CheckAttributeLength(2, attributeCount, command);
                        _DWidth = ParseSize(values[1], values[2], command);
                        break;
                    case "SWIDTH1":
                        CheckAttributeLength(2, attributeCount, command);
                        _SWidth1 = ParseSize(values[1], values[2], command);
                        break;
                    case "DWIDTH1":
                        CheckAttributeLength(2, attributeCount, command);
                        _DWidth1 = ParseSize(values[1], values[2], command);
                        break;
                    case "VVECTOR":
                        CheckAttributeLength(2, attributeCount, command);
                        _VVector = ParsePoint(values[1], values[2], command);
                        break;
                }

            }
        }
    }
}