using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Color
    {
        protected Dictionary<string, (byte, byte, byte)> colorMap = new Dictionary<string, (byte, byte, byte)>
        {
            {"White",(255,255,255)},
            {"Black",(0,0,0)},
            {"Red",(255,0,0) },
            {"Green",(0,255,0) },
            {"Blue",(0,0,255) },
            {"Yellow",(255,255,0) }
        };
        protected (byte, byte, byte) rgbColor;
        public (byte, byte, byte) RGBColor { get { return rgbColor; } }

        public Color(byte r, byte g, byte b)
        {
            rgbColor.Item1 = r;
            rgbColor.Item2 = g;
            rgbColor.Item3 = b;
        }
        public Color(string colorName)
        {

            if (!colorMap.TryGetValue(colorName, out rgbColor))
                throw new Exception("Invalid color");
        }
    }
}
