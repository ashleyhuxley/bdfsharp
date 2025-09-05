using System.Drawing;

namespace ElectricFox.BdfFontLib
{
    public sealed class BdfGeometry
    {
        public Size? ScalableWidth { get; init; }
        public Size? DeviceWidth { get; init; }
        public Size? ScalableWidth1 { get; init; }
        public Size? DeviceWidth1 { get; init; }
        public Point? VVector { get; init; }
    }
}
