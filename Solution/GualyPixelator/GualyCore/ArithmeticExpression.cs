using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class ArithmeticExpression : Expression
    {

        protected ArithmeticExpression(CodeLocation location, Type type) : base(location, type)
        {

        }
        public int ArithmeticEvaluate(ProgramState programState)
        {
            return (int)Evaluate(programState);
        }
    }
}
