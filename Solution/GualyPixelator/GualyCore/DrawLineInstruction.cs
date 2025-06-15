using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class DrawLineInstruction : Instruction
    {
        public DrawLineInstruction(CodeLocation location, List<Expression> parameters) : base(location, parameters)
        {
        }

        public override void Execute(ProgramState programState)
        {
            int varX = (int)parameters[0].Evaluate(programState);
            int varY = (int)parameters[1].Evaluate(programState);
            int distance = (int)parameters[2].Evaluate(programState);
            if ((varX != 0 && varX != 1 && varX != -1) || (varY != 0 && varY != 1 && varY != -1))
                throw new Exception("Invalid directions");
            if(distance < 0)
                throw new Exception("Distance must be greater than or equal 0");
            for (int i = 0; i <= distance; i++ )
            {
                if (i > 0)
                {
                    programState.wallePosition = (programState.wallePosition.Item1 + varX, programState.wallePosition.Item2 + varY);
                }
                programState.Draw();
                
                if (!programState.IsInRange(varX, varY))
                    break;
            }

        }

    }
}
