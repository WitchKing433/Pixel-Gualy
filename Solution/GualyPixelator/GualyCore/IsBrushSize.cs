using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class IsBrushSize : Function
    {
        public IsBrushSize(CodeLocation location, Type type, List<Expression> parameters) : base(location, type, parameters)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            return (int)parameters[0].Evaluate(programState) == programState.brushSize ? 1 : 0;
        }
    }
}
