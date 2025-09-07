namespace ElectricFox.BdfFontLib
{
    internal class BdfCharacterLoader : BdfLoader
    {
        private bool isReadingBitmap;

        private static List<int> usedEncodingValues = new();

        private string _name;
        private int _encoding = -1;
        private int _encodingNonstandard = -1;
        private BdfBoundingBox? _CBB;
        private List<byte[]> bytes = new();

        public BdfCharacterLoader(string name)
        {
            _name = name;
        }

        public BdfGlyph GetCharacter()
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

            var geommetry = new BdfGeometry
            {
                ScalableWidth = _SWidth,
                DeviceWidth = _DWidth,
                ScalableWidth1 = _SWidth1,
                DeviceWidth1 = _DWidth1,
                VVector = _VVector,
            };

            return new BdfGlyph(
                _name,
                encoding,
                ParseBooleanData(bytes))
            {
                Geometry = geommetry,
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

            bool[,] result = new bool[cols, rows];

            for (int y = 0; y < rows; y++)
            {
                if (y >= data.Count)
                {
                    break;
                }

                byte[] rowData = data[y];

                for (int x = 0; x < cols; x++)
                {
                    int byteIndex = x / 8;
                    int bitIndex = 7 - (x % 8); // Bits are typically stored from MSB to LSB
                    if (byteIndex < rowData.Length)
                    {
                        result[x, y] = (rowData[byteIndex] & (1 << bitIndex)) != 0;
                    }
                    else
                    {
                        result[x, y] = false; // Padding with false if data is insufficient
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
                    case "BITMAP":
                        isReadingBitmap = true;
                        break;
                }
            }

            return false;
        }
    }
}