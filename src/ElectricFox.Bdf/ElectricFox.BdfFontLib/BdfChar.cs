using System.Drawing;

namespace ElectricFox.BdfFontLib
{
    public class BdfChar
    {
        private bool[,] _data;

        public BdfChar(string name, int encoding, bool[,] data)
        {
            Name = name;
            Encoding = encoding;
            _data = data;
        }

        public string Name { get; }
        public int Encoding { get; }
        public Size? ScalableWidth { get; init; }
        public Size? DeviceWidth { get; init; }
        public Size? ScalableWidth1 { get; init; }
        public Size? DeviceWidth1 { get; init; }
        public Point? VVector { get; init; }
        public required BdfBoundingBox BoundingBox { get; init; }
        public bool this[int row, int col] => _data[row, col];
    }
}
