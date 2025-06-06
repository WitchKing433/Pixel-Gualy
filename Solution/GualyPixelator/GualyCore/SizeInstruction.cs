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

        public override ProgramState Execute(ProgramState programState)
        {
            throw new NotImplementedException();
        }
    }
}
