using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class GreaterEqualComparator : Comparator, Expression
    {
        public override object Evaluate()
        {
            return leftOperand.ArithmeticEvaluate() >= rightOperand.ArithmeticEvaluate();
        }
    }
}
