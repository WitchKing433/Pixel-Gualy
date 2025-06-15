using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class IsCanvasColor : Function
    {
        public IsCanvasColor(CodeLocation location, Type type, List<Expression> parameters) : base(location, type, parameters)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            try
            {
                int targetX = programState.wallePosition.Item1 + (int)parameters[2].Evaluate(programState);
                int targetY = programState.wallePosition.Item2 + (int)parameters[1].Evaluate(programState);
                return programState.EqualColor(parameters[0].Evaluate(programState).ToString(), programState.canvas[targetX,targetY]) ? 1 : 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
