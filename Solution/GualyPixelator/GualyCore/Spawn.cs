using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Spawn : Instruction
    {
        public Spawn(CodeLocation location, List<IExpression> parameters) : base(location,parameters)
        {

        }

        public override Color[,] ChangePixelMap(Color[,] previusPixelMap)
        {
            throw new NotImplementedException();
        }

    }
}
