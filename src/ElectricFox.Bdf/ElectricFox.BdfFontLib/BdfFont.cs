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
        public required IReadOnlyDictionary<int, BdfGlyph> Glyphs { get; init; }
        public required BdfGeometry Geometry { get; init; }

        public static async Task<BdfFont> LoadAsync(string fileName)
        {
            var loader = new BdfFontLoader();
            return await loader.LoadAsync(fileName);
        }

        public Rectangle MeasureString(string text)
        {
            return MeasureString(text.Select(c => (int)c));
        }

        public Rectangle MeasureString(IEnumerable<int> values)
        {
            int minX = 0, minY = 0, maxX = 0, maxY = 0;

            Point origin = new(0, 0);

            foreach (int c in values)
            {
                if (Glyphs.TryGetValue(c, out BdfGlyph? glyph))
                {
                    minX = Math.Min(minX, origin.X - glyph.BoundingBox.XOffset);
                    maxX = Math.Max(maxX, origin.X + glyph.BoundingBox.XOffset + glyph.BoundingBox.Width);

                    // Y axis is reversed
                    minY = Math.Min(minY, origin.Y - glyph.BoundingBox.Height);
                    maxY = Math.Max(maxY, origin.Y - glyph.BoundingBox.YOffset);

                    if (glyph.Geometry.DeviceSize.HasValue)
                    {
                        origin.X += glyph.Geometry.DeviceSize.Value.Width;
                        origin.Y += glyph.Geometry.DeviceSize.Value.Height;
                    }
                    else if (this.Geometry.DeviceSize.HasValue)
                    {
                        origin.X += this.Geometry.DeviceSize.Value.Width;
                        origin.X += this.Geometry.DeviceSize.Value.Height;
                    }
                    else
                    {
                        throw new InvalidOperationException("No device width specified for character or font");
                    }
                }
            }

            return Rectangle.FromLTRB(minX, minY, maxX, maxY);
        }


        public bool[,] RenderBitmap(string text)
        {
            var size = MeasureString(text);
            var result = new bool[size.Width, size.Height];
            Point origin = new(0 - size.X, 0 - size.Y);

            foreach (char c in text)
            {
                if (Glyphs.TryGetValue((int)c, out BdfGlyph? glyph))
                {
                    for (int row = 0; row < glyph.Height; row++)
                    {
                        for (int col = 0; col < glyph.Width; col++)
                        {
                            if (glyph[col, row])
                            {
                                int x = origin.X + glyph.BoundingBox.XOffset + col;
                                int y = (origin.Y - glyph.BoundingBox.Height) - glyph.BoundingBox.YOffset + row;
                                if (x >= 0 && x < size.Width && y >= 0 && y < size.Height)
                                {
                                    result[x, y] = true;
                                }
                            }
                        }
                    }
                    if (glyph.Geometry.DeviceSize.HasValue)
                    {
                        origin.X += glyph.Geometry.DeviceSize.Value.Width;
                        origin.Y += glyph.Geometry.DeviceSize.Value.Height;
                    }
                    else if (this.Geometry.DeviceSize.HasValue)
                    {
                        origin.X += this.Geometry.DeviceSize.Value.Width;
                        origin.Y += this.Geometry.DeviceSize.Value.Height;
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
