using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class DrawRectangleInstruction : Instruction
    {
        public DrawRectangleInstruction(CodeLocation location, List<Expression> parameters) : base(location, parameters)
        {
        }

        public override void Execute(ProgramState programState)
        {
            int varX = (int)parameters[0].Evaluate(programState);
            int varY = (int)parameters[1].Evaluate(programState);
            int distance = (int)parameters[2].Evaluate(programState);
            int width = (int)parameters[3].Evaluate(programState);
            int height = (int)parameters[4].Evaluate(programState);
            (int, int) futurePosition = (0, 0);
            if ((varX != 0 && varX != 1 && varX != -1) || (varY != 0 && varY != 1 && varY != -1))
                throw new Exception("Invalid directions");
            if (distance < 0)
                throw new Exception("Distance must be greater than or equal 0");
            for (int i = 0; i <= distance; i++)
            {
                if (i > 0)
                {
                    programState.wallePosition = (programState.wallePosition.Item1 + varX, programState.wallePosition.Item2 + varY);
                }
                if (programState.IsInRange() && !programState.IsInRange(varX, varY))
                    futurePosition = programState.wallePosition;
            }
            (int centerX, int centerY) = programState.wallePosition;
            for (int i = -width / 2; i <= width / 2; i++)
            {
                programState.DrawAtPosition(centerX + i, centerY - height / 2);
                programState.DrawAtPosition(centerX + i, centerY + height / 2);
            }
            for (int j = -height / 2; j <= height / 2; j++)
            {
                programState.DrawAtPosition(centerX - width / 2, centerY + j);
                programState.DrawAtPosition(centerX + width / 2, centerY + j);
            }
            programState.wallePosition = (centerX, centerY);
            if (!programState.IsInRange())
            {
                programState.wallePosition = futurePosition;
            }
        }
    }
}
