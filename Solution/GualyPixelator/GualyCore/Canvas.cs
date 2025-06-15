using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Canvas
    {
        Color[,] map;
        public Canvas(int x, int y)
        {
            map = new Color[y, x];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i,j] = Color.White;
                }
            }
        }
        public int Width => map.GetLength(1);
        public int Height => map.GetLength(0);

        public Color this[int x, int y]
        {
            get 
            {
                if (x > Width || x < 0)
                {
                    throw new Exception("The first component is outside the canvas");
                }
                if (y > Height || y < 0)
                {
                    throw new Exception("The second component is outside the canvas");
                }
                return map[y, x];
            }
            set
            {
                if (x > Width || x < 0)
                {
                    throw new Exception("The first component is outside the canvas");
                }
                if (y > Height || y < 0)
                {
                    throw new Exception("The second component is outside the canvas");
                }
                map[y, x] = value;
            }
        }
    }
}
