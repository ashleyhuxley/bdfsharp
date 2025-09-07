using System.Drawing;

namespace ElectricFox.BdfSharp
{
    public sealed class BdfGeometry
    {
        public Size? DeviceSize { get; init; }
        public Size? ScalableSize { get; init; }
        public Size? DeviceSize1 { get; init; }
        public Size? ScalableSize1 { get; init; }
        public Point? VVector { get; init; }
    }
}
