using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class GetCanvasSize : Function
    {
        public GetCanvasSize(CodeLocation location, Type type, List<Expression> parameters) : base(location, type, parameters)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            return programState.canvas.Width;
        }
    }
}
