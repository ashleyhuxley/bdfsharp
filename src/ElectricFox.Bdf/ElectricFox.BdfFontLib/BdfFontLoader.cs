using ElectricFox.BdfViewer;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;

namespace ElectricFox.BdfFontLib
{
    internal class BdfFontLoader
    {
        private int currentChar = -1;

        private bool readingProperties;

        private string? _fontName;
        private string? _version;
        private BdfSize? _size;
        private BdfBoundingBox? _fontBoundingBox;
        private int _charCount;
        private readonly Dictionary<string, string> _properties = new();

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

            return new BdfFont
                {
                    FontName = _fontName ?? throw new BdfLoadException("Font name was not specified"),
                    Version = _version ?? string.Empty,
                    Size = _size ?? throw new BdfLoadException("SIZE was not specified"),
                    FontBoundingBox = _fontBoundingBox ?? throw new BdfLoadException("Bounding Box was not specified"),
                    CharCount = _charCount,
                    Properties = new Dictionary<string, string>(_properties)
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

        bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
        {
            // Look for a EOL in the buffer.
            SequencePosition? position = buffer.PositionOf((byte)'\n');

            if (position == null)
            {
                line = default;
                return false;
            }

            // Skip the line + the \n.
            line = buffer.Slice(0, position.Value);
            buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
            return true;
        }

        private void CheckAttributeLength(int expected, int actual, string command)
        {
            if (expected != actual)
            {
                throw new BdfLoadException($"Invalid number of attributes for {command}. Expected {expected}, got {actual}");
            }
        }

        private void ProcessLine(ReadOnlySequence<byte> line)
        {
            // Convert the line to a string and process it.
            var lineString = Encoding.UTF8.GetString(line.ToArray());
            var values = lineString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (values.Length == 0)
            {
                return;
            }

            var command = values[0].ToUpperInvariant();
            var attributeCount = values.Length - 1;

            if (currentChar == -1)
            {
                if (readingProperties)
                {
                    _properties[command] = lineString[(lineString.IndexOf(' ') + 1)..].Trim();
                }
                else
                {
                    switch (command)
                    {
                        case "STARTFONT":
                            CheckAttributeLength(1, attributeCount, command);
                            _version = values[1];
                            break;
                        case "STARTCHAR":
                            CheckAttributeLength(1, attributeCount, command);
                            if (int.TryParse(values[1], out int charCode))
                            {
                                currentChar = charCode;
                                //font.Characters[charCode] = new BdfCharacter { CharCode = charCode };
                            }
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
                            readingProperties = true;
                            _properties.Clear();
                            break;
                        case "ENDPROPERTIES":
                            CheckAttributeLength(0, attributeCount, command);
                            readingProperties = false;
                            break;
                    }

                }
            }
        }
    }
}
