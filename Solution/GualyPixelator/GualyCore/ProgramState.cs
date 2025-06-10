using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GualyCore
{
    public class ProgramState
    {
        public int index;
        public Color[,] pixelMap;
        public int brushSize;
        public Color brushColor;
        public (int, int) wallePosition;
        public Dictionary<string, int> labels;
        public Dictionary<string, object> variables;

        public ProgramState(int index, Color[,] pixelMap, int brushSize, Color brushColor, (int, int) wallePosition)
        {
            this.index = index;
            this.pixelMap = pixelMap;
            this.brushSize = brushSize;
            this.brushColor = brushColor;
            this.wallePosition = wallePosition;
            labels = new Dictionary<string, int> ();
            variables = new Dictionary<string, object> ();
        }
    }
}
