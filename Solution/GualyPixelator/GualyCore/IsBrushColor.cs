using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GualyCore
{
    public class IsBrushColor : Function
    {
        public IsBrushColor(CodeLocation location, Type type, List<Expression> parameters) : base(location, type, parameters)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            return programState.IsBrushColor(parameters[0].Evaluate(programState).ToString()) ? 1 : 0;
        }
    }
}
