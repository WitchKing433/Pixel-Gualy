using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class DrawCircleInstruction : Instruction
    {
        public DrawCircleInstruction(CodeLocation location, List<Expression> parameters) : base(location, parameters)
        {
        }

        public override void Execute(ProgramState programState)
        {
            int varX = (int)parameters[0].Evaluate(programState);
            int varY = (int)parameters[1].Evaluate(programState);
            int radius = (int)parameters[2].Evaluate(programState);
            (int, int) futurePosition = (0,0);
            if ((varX != 0 && varX != 1 && varX != -1) || (varY != 0 && varY != 1 && varY != -1))
                throw new Exception("Invalid directions");
            if (radius <= 0)
                throw new Exception("Radius must be greater than 0");
            for (int i = 0; i <= radius; i++)
            {
                if (i > 0)
                {
                    programState.wallePosition = (programState.wallePosition.Item1 + varX, programState.wallePosition.Item2 + varY);
                }
                if (programState.IsInRange() && !programState.IsInRange(varX, varY))
                    futurePosition = programState.wallePosition;
            }
            (int centerX, int centerY) = programState.wallePosition;
            int x = radius;
            int y = 0;
            int decisionOver2 = 3 - (2 * x);

            while (x >= y)
            {
                programState.DrawAtPosition(centerX + x, centerY + y);
                programState.DrawAtPosition(centerX - x, centerY + y);
                programState.DrawAtPosition(centerX + x, centerY - y);
                programState.DrawAtPosition(centerX - x, centerY - y);
                programState.DrawAtPosition(centerX + y, centerY + x);
                programState.DrawAtPosition(centerX - y, centerY + x);
                programState.DrawAtPosition(centerX + y, centerY - x);
                programState.DrawAtPosition(centerX - y, centerY - x);

                y++;
                if (decisionOver2 <= 0)
                {
                    decisionOver2 += 4 * y + 6;
                }
                else
                {
                    x--;
                    decisionOver2 += 4 * (y - x) + 10;
                }
            }

            programState.wallePosition = (centerX, centerY);
            if (!programState.IsInRange())
            {
                programState.wallePosition = futurePosition;
            }
        }
       
    }
}
