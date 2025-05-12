using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class ArithmeticExpression : LogicExpression
    {
        protected ArithmeticExpression(CodeLocation location) : base(location)
        {

        }

        public int ArithmeticEvaluate()
        {
            return (int) Evaluate();
        }

        public override bool LogicEvaluate()
        {
            return ArithmeticEvaluate() != 0;
        }
    }
}
