using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    internal class AndOperator : LogicOperator, IExpression
    {
        public override object Evaluate()
        {
            return leftOperand.LogicEvaluate() && rightOperand.LogicEvaluate();
        }
    }
}
