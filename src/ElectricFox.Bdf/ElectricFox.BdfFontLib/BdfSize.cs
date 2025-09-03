namespace ElectricFox.BdfFontLib
{
    public class BdfSize
    {
        public int PointSize { get; internal set; }
        public int XResolution { get; internal set; }
        public int YResolution { get; internal set; }

        public override string ToString()
        {
            return $"{PointSize} {XResolution} {YResolution}";
        }

        public static BdfSize Parse(string pointSize, string xResolution, string yResolution)
        {
            if (!int.TryParse(pointSize, out int ps))
            {
                throw new BdfLoadException("Invalid point size in SIZE specification");
            }

            if (!int.TryParse(xResolution, out int xr))
            {
                throw new BdfLoadException("Invalid X resolution in SIZE specification");
            }

            if (!int.TryParse(yResolution, out int yr))
            {
                throw new BdfLoadException("Invalid Y resolution in SIZE specification");
            }

            return new BdfSize
            {
                PointSize = ps,
                XResolution = xr,
                YResolution = yr
            };
        }
    }
}
