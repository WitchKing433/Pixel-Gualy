using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class SizeInstruction : Instruction
    {
        public SizeInstruction(CodeLocation location, List<Expression> parameters) : base(location, parameters)
        {
        }

        public override void Execute(ProgramState programState)
        {
            int size = (int)parameters[0].Evaluate(programState);
            if (size > 0)
            {
                programState.brushSize = size % 2 == 0 ? size - 1 : size;
            }
            else
                throw new Exception("Brush size must be greater than 0");
        }
    }
}
