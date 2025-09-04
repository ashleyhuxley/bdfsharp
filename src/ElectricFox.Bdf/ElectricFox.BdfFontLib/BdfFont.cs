using ElectricFox.BdfFontLib;

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
    }
}
