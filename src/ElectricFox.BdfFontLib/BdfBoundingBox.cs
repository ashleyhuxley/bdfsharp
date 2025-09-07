namespace ElectricFox.BdfFontLib
{
    public class BdfBoundingBox
    {
        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public int XOffset { get; internal set; }
        public int YOffset { get; internal set; }

        public override string ToString()
        {
            return $"{Width}x{Height}+{XOffset}+{YOffset}";
        }

        public static BdfBoundingBox Parse(string width, string height, string xoff, string yoff)
        {
            if (!int.TryParse(width, out int w))
            {
                throw new BdfLoadException("Invalid width size in FONTBOUNDINGBOX specification");
            }

            if (!int.TryParse(height, out int h))
            {
                throw new BdfLoadException("Invalid height in FONTBOUNDINGBOX specification");
            }

            if (!int.TryParse(xoff, out int xo))
            {
                throw new BdfLoadException("Invalid X offset in FONTBOUNDINGBOX specification");
            }

            if (!int.TryParse(yoff, out int yo))
            {
                throw new BdfLoadException("Invalid Y offset in FONTBOUNDINGBOX specification");
            }

            return new BdfBoundingBox
            {
                Width = w,
                Height = h,
                XOffset = xo,
                YOffset = yo
            };
        }
    }
}
