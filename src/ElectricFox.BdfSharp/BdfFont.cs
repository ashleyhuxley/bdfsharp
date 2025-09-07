using System.Drawing;

namespace ElectricFox.BdfSharp
{
    public class BdfFont
    {
        public required string FontName { get; init; }
        public required string Version { get; init; }
        public required BdfSize Size { get; init; }
        public required BdfBoundingBox FontBoundingBox { get; init; }
        public required int CharCount { get; init; }
        public required Dictionary<string, string> Properties { get; init; }
        public required IReadOnlyList<BdfGlyph> Glyphs { get; init; }
        public required BdfGeometry Geometry { get; init; }

        public static async Task<BdfFont> LoadAsync(string fileName)
        {
            var loader = new BdfFontLoader();
            return await loader.LoadAsync(fileName);
        }

        /// <summary>
        /// Get the dimensions of the rendered string, using "best guess" for glyph lookup
        /// </summary>
        /// <param name="text">The string to measure</param>
        /// <returns>A rectangle whose origin (0, 0) is the baseline of the starting character</returns>
        public Rectangle MeasureString(string text)
        {
            return MeasureString(text.EnumerateRunes().Select(r => r.Value));
        }

        /// <summary>
        /// Get the dimensions of the rendered string
        /// </summary>
        /// <param name="values">Values of the glyphs to measure</param>
        /// <param name="glyphLookupOption">Specifies how to look up the glyph based on the values</param>
        /// <returns>A rectangle whose origin (0, 0) is the baseline of the starting character</returns>
        /// <exception cref="InvalidOperationException">The glyph or font must have Device dimensions specified (DWIDTH). If neither of these are present, an exception is thrown.</exception>
        public Rectangle MeasureString(IEnumerable<int> values, GlyphLookupOption glyphLookupOption = GlyphLookupOption.BestGuess)
        {
            int minX = 0,
                minY = 0,
                maxX = 0,
                maxY = 0;

            Point origin = new(0, 0);

            foreach (int c in values)
            {
                var glyph = LookupGlyph(c, glyphLookupOption);

                if (glyph != null)
                {
                    minX = Math.Min(minX, origin.X + glyph.BoundingBox.XOffset);
                    maxX = Math.Max(
                        maxX,
                        origin.X + glyph.BoundingBox.XOffset + glyph.BoundingBox.Width
                    );

                    // Y axis is reversed
                    var yOffset = origin.Y - (glyph.BoundingBox.Height + glyph.BoundingBox.YOffset);
                    minY = Math.Min(minY, yOffset);
                    maxY = Math.Max(maxY, yOffset + glyph.BoundingBox.Height);

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
                        throw new InvalidOperationException(
                            "No device width specified for character or font"
                        );
                    }
                }
            }

            return Rectangle.FromLTRB(minX, minY, maxX, maxY);
        }

        /// <summary>
        /// Render the specified values to a bitmap (2D bool array)
        /// </summary>
        /// <param name="values">Values of the glyphs to render</param>
        /// <param name="glyphLookupOption">Specifies how to look up the glyph based on the values</param>
        /// <returns>A two dimensional boolean array representing the text rendered in the specified font.</returns>
        /// <exception cref="InvalidOperationException">The glyph or font must have Device dimensions specified (DWIDTH). If neither of these are present, an exception is thrown.</exception>
        public bool[,] RenderBitmap(IEnumerable<int> values, GlyphLookupOption glyphLookupOption = GlyphLookupOption.BestGuess)
        {
            var size = MeasureString(values, glyphLookupOption);
            var result = new bool[size.Width, size.Height];
            Point origin = new(0 - size.X, 0 - size.Y);

            foreach (var c in values)
            {
                var glyph = Glyphs.FirstOrDefault(g => g.Encoding == c);

                if (glyph != null)
                {
                    for (int row = 0; row < glyph.Height; row++)
                    {
                        for (int col = 0; col < glyph.Width; col++)
                        {
                            if (glyph[col, row])
                            {
                                int x = origin.X + glyph.BoundingBox.XOffset + col;
                                int y =
                                    (origin.Y - glyph.BoundingBox.Height)
                                    - glyph.BoundingBox.YOffset
                                    + row;
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
                        throw new InvalidOperationException(
                            "No device width specified for character or font"
                        );
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Render the specified text to a bitmap (2D bool array) using "best guess" for glyph lookup
        /// </summary>
        /// <param name="text">The text to render</param>
        /// <returns>A two dimensional boolean array representing the text rendered in the specified font.</returns>
        /// <exception cref="InvalidOperationException">The glyph or font must have Device dimensions specified (DWIDTH). If neither of these are present, an exception is thrown.</exception>
        public bool[,] RenderBitmap(string text)
        {
            return RenderBitmap(text.EnumerateRunes().Select(r => r.Value));
        }

        private BdfGlyph? LookupGlyph(int value, GlyphLookupOption option)
        {
            switch (option)
            {
                case GlyphLookupOption.EncodingStrict:
                    return Glyphs.FirstOrDefault(g => g.Encoding == value);
                case GlyphLookupOption.UseIndex:
                    if (value >= 0 && value < Glyphs.Count)
                    {
                        return Glyphs[value];
                    }

                    return null;
                default:
                    var glyph = Glyphs.FirstOrDefault(g => g.Encoding == value);
                    if (glyph != null)
                    {
                        return glyph;
                    }

                    // Best guess by parsing STARTCHAR
                    return Glyphs.FirstOrDefault(g =>
                        !string.IsNullOrEmpty(g.Name)
                        && (
                            (g.Name.Length == 1 && g.Name[0] == (char)value)
                            || (
                                g.Name.StartsWith("uni", StringComparison.OrdinalIgnoreCase)
                                && int.TryParse(
                                    g.Name.Substring(3),
                                    System.Globalization.NumberStyles.HexNumber,
                                    null,
                                    out int uniCode
                                )
                                && uniCode == value
                            )
                            || (
                                g.Name.StartsWith("U+", StringComparison.OrdinalIgnoreCase)
                                && int.TryParse(
                                    g.Name.Substring(2),
                                    System.Globalization.NumberStyles.HexNumber,
                                    null,
                                    out int uniCode2
                                )
                                && uniCode2 == value
                            )
                        )
                    );
            }
        }
    }
}
