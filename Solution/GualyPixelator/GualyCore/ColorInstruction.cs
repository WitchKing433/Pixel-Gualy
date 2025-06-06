using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class ColorInstruction : Instruction
    {
        public ColorInstruction(CodeLocation location, List<Expression> parameters) : base(location, parameters)
        {

        }
        public override ProgramState Execute(ProgramState programState)
        {
            throw new NotImplementedException();
        }
    }
}
