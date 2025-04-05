using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class ArithmeticExpression : IExpression
    {
        public abstract object Evaluate();

        public int ArithmeticEvaluate()
        {
            return (int) Evaluate();
        }
    }
}
