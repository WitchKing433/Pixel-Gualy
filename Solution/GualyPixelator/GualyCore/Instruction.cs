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
            switch (instruction)
            {
                case "Color": { return new ColorInstruction(location, parameters); }

                case "Size": { return new SizeInstruction(location, parameters); }

                case "DrawLine": { return new DrawLineInstruction(location, parameters); }

                case "DrawCircle": { return new ColorInstruction(location, parameters); }

                case "DrawRectangle": { return new DrawRectangleInstruction(location, parameters); }

                case "Fill": { return new FillInstruction(location, parameters); }
                    
                default : { throw new Exception("Invalid Instruction"); }
            }
        }
        public abstract ProgramState Execute(ProgramState programState);

    }
}
