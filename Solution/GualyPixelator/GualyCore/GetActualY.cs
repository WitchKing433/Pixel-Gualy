using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class GetActualY : Function
    {
        public GetActualY(CodeLocation location, Type type, List<Expression> parameters) : base(location, type, parameters)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            return programState.wallePosition.Item2;
        }
    }
}
