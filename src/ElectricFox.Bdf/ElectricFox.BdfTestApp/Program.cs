using ElectricFox.BdfViewer;

namespace ElectricFox.BdfTestApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var font = await BdfFont.LoadAsync("Z:\\Assets\\BDF Fonts\\4x6.bdf");
            foreach (var attrib in font.Properties)
            {
                Console.WriteLine($"{attrib.Key} = {attrib.Value}");
            }
        }
    }
}
