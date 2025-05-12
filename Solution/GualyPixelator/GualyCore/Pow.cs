using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Pow : ArithmeticOperator
    {
        public Pow(CodeLocation location) : base(location)
        {
        }

        public override object Evaluate()
        {
            return Math.Pow(LeftOperand.ArithmeticEvaluate(), RightOperand.ArithmeticEvaluate());
        }
    }
}
