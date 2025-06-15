using System;
using System.Collections.Generic;
using System.Drawing;
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
        public override void Execute(ProgramState programState)
        {
            string colorName = parameters[0].Evaluate(programState).ToString();
            Color color = Color.FromName(colorName);
            if (color.IsKnownColor)
            {
                programState.brushColor = color;
            }
            else
            {
                throw new Exception("Invalid color");             
            }
        }
    }
}
