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
            programState.wallePosition = (x,y);
        }
    }
}
