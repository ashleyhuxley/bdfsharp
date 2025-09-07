using System.Drawing;

namespace ElectricFox.BdfFontLib
{
    public class BdfFont
    {
        public required string FontName { get; init; }
        public required string Version { get; init; }
        public required BdfSize Size { get; init; }
        public required BdfBoundingBox FontBoundingBox { get; init; }
        public required int CharCount { get; init; }
        public required Dictionary<string, string> Properties { get; init; }
        public required IReadOnlyDictionary<int, BdfGlyph> Chars { get; init; }
        public required BdfGeometry Geometry { get; init; }

        public static async Task<BdfFont> LoadAsync(string fileName)
        {
            var loader = new BdfFontLoader();
            return await loader.LoadAsync(fileName);
        }

        public Size MeasureString(string text)
        {
            return MeasureString(text.Select(c => (int)c));
        }

        public Size MeasureString(IEnumerable<int> values)
        {
            int minX = 0, minY = 0, maxX = 0, maxY = 0;

            Point origin = new(0, 0);

            foreach (int c in values)
            {
                if (Chars.TryGetValue(c, out BdfGlyph? bdfChar))
                {
                    minX = Math.Min(minX, origin.X + bdfChar.BoundingBox.XOffset);
                    minY = Math.Min(minY, origin.Y + bdfChar.BoundingBox.YOffset);
                    maxX = Math.Max(maxX, origin.X + bdfChar.BoundingBox.XOffset + bdfChar.BoundingBox.Width);
                    maxY = Math.Max(maxY, origin.Y + bdfChar.BoundingBox.YOffset + bdfChar.BoundingBox.Height);

                    if (bdfChar.Geometry.DeviceWidth.HasValue)
                    {
                        origin.X += bdfChar.Geometry.DeviceWidth.Value.Width;
                        origin.Y += bdfChar.Geometry.DeviceWidth.Value.Height;
                    }
                    else if (this.Geometry.DeviceWidth.HasValue)
                    {
                        origin.X += this.Geometry.DeviceWidth.Value.Width;
                        origin.X += this.Geometry.DeviceWidth.Value.Height;
                    }
                    else
                    {
                        throw new InvalidOperationException("No device width specified for character or font");
                    }
                }
            }

            return new Size(maxX - minX, maxY - minY);
        }


        public bool[,] RenderBitmap(string text)
        {
            var size = MeasureString(text);
            var result = new bool[size.Width, size.Height];
            Point origin = new(0, 0);
            foreach (char c in text)
            {
                if (Chars.TryGetValue((int)c, out BdfGlyph? bdfChar))
                {
                    for (int row = 0; row < bdfChar.BoundingBox.Height; row++)
                    {
                        for (int col = 0; col < bdfChar.BoundingBox.Width; col++)
                        {
                            if (bdfChar[col, row])
                            {
                                int x = origin.X + bdfChar.BoundingBox.XOffset + col;
                                int y = origin.Y + bdfChar.BoundingBox.YOffset + row;
                                if (x >= 0 && x < size.Width && y >= 0 && y < size.Height)
                                {
                                    result[x, y] = true;
                                }
                            }
                        }
                    }
                    if (bdfChar.Geometry.DeviceWidth.HasValue)
                    {
                        origin.X += bdfChar.Geometry.DeviceWidth.Value.Width;
                        origin.Y += bdfChar.Geometry.DeviceWidth.Value.Height;
                    }
                    else if (this.Geometry.DeviceWidth.HasValue)
                    {
                        origin.X += this.Geometry.DeviceWidth.Value.Width;
                        origin.Y += this.Geometry.DeviceWidth.Value.Height;
                    }
                    else
                    {
                        throw new InvalidOperationException("No device width specified for character or font");
                    }
                }
            }

            return result;
        }
    }
}
