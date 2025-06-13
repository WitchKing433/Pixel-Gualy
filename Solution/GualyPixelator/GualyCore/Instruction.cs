using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class Instruction : ILineNode
    {
        
        public CodeLocation Location {  get; private  set; }
        protected List<Expression> parameters;

        public Instruction(CodeLocation location, List<Expression> parameters)
        {
            Location = location;
            this.parameters = parameters;
        }
        public static Instruction BuildInstruction(CodeLocation location, List<Expression> parameters, string instruction)
        {
            if(ProgramData.instructions.TryGetValue(instruction, out Type type))
            {
                return (Instruction)Activator.CreateInstance(type, location, parameters);
            }
            throw new Exception("Invalid Instruction");

        }
        public abstract ProgramState Execute(ProgramState programState);

    }
}
