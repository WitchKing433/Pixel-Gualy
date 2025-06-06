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

        public override ProgramState Execute(ProgramState programState)
        {
            throw new NotImplementedException();
        }
    }
}
