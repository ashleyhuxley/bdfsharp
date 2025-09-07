namespace ElectricFox.BdfSharp
{
    internal sealed class BdfGlyphLoader : BdfLoader
    {
        private bool isReadingBitmap;

        private readonly string _name;
        private int? _encoding = default;
        private BdfBoundingBox? _CBB;
        private readonly List<byte[]> bytes = [];

        public BdfGlyphLoader(string name)
        {
            _name = name;
        }

        public BdfGlyph GetGlyph()
        {
            if (_CBB == null)
            {
                throw new BdfLoadException($"Glyph '{_name}' missing BBX");
            }

            var geommetry = new BdfGeometry
            {
                ScalableSize = _SWidth,
                DeviceSize = _DWidth,
                ScalableSize1 = _SWidth1,
                DeviceSize1 = _DWidth1,
                VVector = _VVector,
            };

            return new BdfGlyph(
                _name,
                _encoding,
                ParseBooleanData(bytes, _CBB))
            {
                Geometry = geommetry,
                BoundingBox = _CBB
            };
        }

        private static bool[,] ParseBooleanData(List<byte[]> data, BdfBoundingBox boundingBox)
        {
            int rows = boundingBox.Height;
            int cols = boundingBox.Width;

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

        public bool ProcessLine(string line)
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
                        if (int.TryParse(values[1], out var encoding))
                        {
                            _encoding = encoding;
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