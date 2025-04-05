using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class Instruction : ILineNode
    {
        public abstract Color[,] ChangePixelMap(Color[,] previusPixelMap);

    }
}
