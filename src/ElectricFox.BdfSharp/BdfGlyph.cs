namespace ElectricFox.BdfSharp
{
    public sealed class BdfGlyph
    {
        private bool[,] _data;

        public BdfGlyph(string name, int? encoding, bool[,] data)
        {
            Name = name;
            Encoding = encoding;
            _data = data;
        }

        public string Name { get; }
        public int? Encoding { get; }
        public required BdfGeometry Geometry { get; init; }
        public required BdfBoundingBox BoundingBox { get; init; }
        public bool this[int row, int col] => _data[row, col];
        public int Width => _data.GetLength(0);
        public int Height => _data.GetLength(1);
    }
}
