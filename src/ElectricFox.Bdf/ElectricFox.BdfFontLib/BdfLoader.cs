using System.Drawing;
using System.Numerics;

namespace ElectricFox.BdfFontLib
{
    public class BdfLoader
    {
        protected Size? _SWidth;
        protected Size? _DWidth;
        protected Size? _SWidth1;
        protected Size? _DWidth1;
        protected Point? _VVector;

        protected void CheckAttributeLength(int expected, int actual, string command)
        {
            if (expected > actual)
            {
                throw new BdfLoadException($"Invalid number of attributes for {command}. Expected {expected}, got {actual}");
            }
        }

        protected Point ParsePoint(string val1, string val2, string command)
        {
            if (!int.TryParse(val1, out int x))
            {
                throw new BdfLoadException($"Invalid {command} X value");
            }

            if (!int.TryParse(val2, out int y))
            {
                throw new BdfLoadException($"Invalid {command} Y value");
            }

            return new Point(x, y);
        }

        protected Size ParseSize(string val1, string val2, string command)
        {
            if (!int.TryParse(val1, out int x))
            {
                throw new BdfLoadException($"Invalid {command} X value");
            }

            if (!int.TryParse(val2, out int y))
            {
                throw new BdfLoadException($"Invalid {command} Y value");
            }

            return new Size(x, y);
        }
    }
}
