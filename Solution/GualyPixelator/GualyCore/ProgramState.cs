using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
