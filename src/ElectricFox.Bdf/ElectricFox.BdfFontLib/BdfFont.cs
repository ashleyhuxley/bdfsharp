using ElectricFox.BdfFontLib;
using System.Drawing;

namespace ElectricFox.BdfViewer
{
    public class BdfFont
    {
        public required string FontName { get; init; }
        public required string Version { get; init; }
        public required BdfSize Size { get; init; }
        public required BdfBoundingBox FontBoundingBox { get; init; }
        public required int CharCount { get; init; }
        public required Dictionary<string, string> Properties { get; init; }
        public required IReadOnlyDictionary<int, BdfChar> Chars { get; init; }

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
            int width = 0;
            int height = 0;
            foreach (int c in values)
            {
                if (Chars.TryGetValue(c, out BdfChar? bdfChar))
                {
                    width += bdfChar.DeviceWidth?.Width ?? bdfChar.BoundingBox.Width;
                    height = Math.Max(height, bdfChar.BoundingBox.Height);
                }
            }
            return new Size(width, height);
        }
    }
}
