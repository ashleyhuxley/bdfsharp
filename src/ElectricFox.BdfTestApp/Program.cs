using ElectricFox.BdfViewer;

namespace ElectricFox.BdfTestApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var font = await BdfFont.LoadAsync("Y:\\Assets\\BDF Fonts\\4x6.bdf");
            foreach (var c in font.Chars.Values)
            {
                Console.WriteLine(c.Name);

                for (int j = 0; j < c.BoundingBox.Height; j++)
                {
                    for (int i = 0; i < c.BoundingBox.Width; i++)
                    {
                        Console.Write(c[j, i] ? '#' : '.');
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    }
}
