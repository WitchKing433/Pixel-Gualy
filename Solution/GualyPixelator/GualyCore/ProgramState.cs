using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;


namespace GualyCore
{
    public class ProgramState
    {
        public int index;
        public Canvas canvas;
        public int brushSize;
        public Color brushColor;
        public (int, int) wallePosition;
        public Dictionary<string, int> labels;
        public Dictionary<string, object> variables;

        public ProgramState((int, int) canvasSize, int brushSize, (int, int) wallePosition, Dictionary<string, int> labels)
        {
            index = 0;
            this.brushSize = brushSize;
            this.wallePosition = wallePosition;
            this.labels = labels;
            variables = new Dictionary<string, object> ();
            canvas = new Canvas(canvasSize.Item1, canvasSize.Item2);
            brushColor = Color.Transparent;
        }

        public void Draw(int x, int y)
        {
            canvas[x, y] = brushColor;
        }
        public bool IsBrushColor(string name)
        {
            return EqualColor(name, brushColor);
        }
        public bool EqualColor(string name, Color color)
        {
            return Color.FromName(name).ToArgb() == color.ToArgb();
        }

        public void CreateImage()
        {
            using(Bitmap bitmap)
        }
    }
}
