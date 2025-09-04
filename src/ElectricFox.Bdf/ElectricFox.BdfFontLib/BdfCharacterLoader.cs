using System.Drawing;

namespace ElectricFox.BdfFontLib
{
    internal class BdfCharacterLoader
    {
        private bool isReadingBitmap;

        private static List<int> usedEncodingValues = new();

        private string _name;
        private int _encoding = -1;
        private int _encodingNonstandard = -1;
        private Size? _SWidth;
        private Size? _DWidth;
        private BdfBoundingBox? _CBB;
        private Size? _SWidth1;
        private Size? _DWidth1;
        private Point? _VVector;
        private List<byte[]> bytes = new();

        public BdfCharacterLoader(string name)
        {
            _name = name;
        }

        private void CheckAttributeLength(int expected, int actual, string command)
        {
            if (expected < actual)
            {
                throw new BdfLoadException($"Invalid number of attributes for {command}. Expected {expected}, got {actual}");
            }
        }

        public BdfChar GetCharacter()
        {
            int encoding;
            if (_encoding == -1 || usedEncodingValues.Contains(_encoding))
            {
                encoding = usedEncodingValues.Count > 0 ? usedEncodingValues.Max() + 1 : 0;
            }
            else
            {
                encoding = _encoding;
            }

            usedEncodingValues.Add(encoding);

            if (_CBB == null)
            {
                throw new BdfLoadException("Character missing BBX");
            }

            return new BdfChar(
                _name,
                encoding,
                ParseBooleanData(bytes))
            {
                ScalableWidth = _SWidth,
                DeviceWidth = _DWidth,
                ScalableWidth1 = _SWidth1,
                DeviceWidth1 = _DWidth1,
                VVector = _VVector,
                BoundingBox = _CBB ?? throw new BdfLoadException($"BBX value not found on character {_name}")
            };
        }

        private bool[,] ParseBooleanData(List<byte[]> data)
        {
            if (_CBB == null)
            {
                throw new BdfLoadException("Character missing BBX");
            }

            int rows = _CBB.Height;
            int cols = _CBB.Width;

            bool[,] result = new bool[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                if (r >= data.Count)
                {
                    break;
                }
                byte[] rowData = data[r];
                for (int c = 0; c < cols; c++)
                {
                    int byteIndex = c / 8;
                    int bitIndex = 7 - (c % 8); // Bits are typically stored from MSB to LSB
                    if (byteIndex < rowData.Length)
                    {
                        result[r, c] = (rowData[byteIndex] & (1 << bitIndex)) != 0;
                    }
                    else
                    {
                        result[r, c] = false; // Padding with false if data is insufficient
                    }
                }
            }

            return result;
        }

        public bool ParseCharacter(string line)
        {
            if (isReadingBitmap)
            {
                if (string.Equals(line.Trim(), "ENDCHAR", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                try
                {
                    bytes.Add(Convert.FromHexString(line.Trim()));
                }
                catch (FormatException ex)
                {
                    throw new BdfLoadException("Invalid bitmap data", ex);
                }
            }
            else
            {
                var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 0)
                {
                    return false;
                }

                var command = values[0].ToUpperInvariant();
                var attributeCount = values.Length - 1;
                switch (command)
                {
                    case "ENDCHAR":
                        return true;
                    case "ENCODING":
                        CheckAttributeLength(1, attributeCount, command);
                        if (!int.TryParse(values[1], out _encoding))
                        {
                            throw new BdfLoadException("Invalid ENCODING value");
                        }
                        break;
                    case "BBX":
                        CheckAttributeLength(4, attributeCount, command);
                        _CBB = BdfBoundingBox.Parse(values[1], values[2], values[3], values[4]);
                        break;
                    case "SWIDTH":
                        CheckAttributeLength(2, attributeCount, command);
                        if (!int.TryParse(values[1], out int swx))
                        {
                            throw new BdfLoadException("Invalid SWIDTH X value");
                        }
                        if (!int.TryParse(values[2], out int swy))
                        {
                            throw new BdfLoadException("Invalid SWIDTH Y value");
                        }
                        _SWidth = new Size(swx, swy);
                        break;
                    case "DWIDTH":
                        CheckAttributeLength(2, attributeCount, command);
                        if (!int.TryParse(values[1], out int dwx))
                        {
                            throw new BdfLoadException("Invalid DWIDTH X value");
                        }
                        if (!int.TryParse(values[2], out int dwy))
                        {
                            throw new BdfLoadException("Invalid DWIDTH Y value");
                        }
                        _DWidth = new Size(dwx, dwy);
                        break;
                    case "SWIDTH1":
                        CheckAttributeLength(2, attributeCount, command);
                        if (!int.TryParse(values[1], out int sw1x))
                        {
                            throw new BdfLoadException("Invalid SWIDTH1 X value");
                        }
                        if (!int.TryParse(values[2], out int sw1y))
                        {
                            throw new BdfLoadException("Invalid SWIDTH1 Y value");
                        }
                        _SWidth1 = new Size(sw1x, sw1y);
                        break;
                    case "DWIDTH1":
                        CheckAttributeLength(2, attributeCount, command);
                        if (!int.TryParse(values[1], out int dw1x))
                        {
                            throw new BdfLoadException("Invalid DWIDTH1 X value");
                        }
                        if (!int.TryParse(values[2], out int dw1y))
                        {
                            throw new BdfLoadException("Invalid DWIDTH1 Y value");
                        }
                        _DWidth1 = new Size(dw1x, dw1y);
                        break;
                    case "VVECTOR":
                        CheckAttributeLength(2, attributeCount, command);
                        if (!int.TryParse(values[1], out int vvx))
                        {
                            throw new BdfLoadException("Invalid VVECTOR X value");
                        }
                        if (!int.TryParse(values[2], out int vvy))
                        {
                            throw new BdfLoadException("Invalid VVECTOR Y value");
                        }
                        _VVector = new Point(vvx, vvy);
                        break;
                    case "BITMAP":
                        isReadingBitmap = true;
                        break;
                }
            }

            return false;
        }
    }
}