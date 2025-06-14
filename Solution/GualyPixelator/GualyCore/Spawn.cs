using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Spawn : Instruction
    {
        public Spawn(CodeLocation location, List<Expression> parameters) : base(location, parameters)
        {
        }

        public override void Execute(ProgramState programState)
        {
            int x = (int)parameters[0].Evaluate(programState);
            int y = (int)parameters[1].Evaluate(programState);
            if(x > programState.pixelMap.GetLength(1) || x < 0)
            {
                throw new Exception("The first component is outside the canvas");
            }
            if (y > programState.pixelMap.GetLength(0) || y < 0)
            {
                throw new Exception("The second component is outside the canvas");
            }
            programState.wallePosition = (y, x);
        }
    }
}
