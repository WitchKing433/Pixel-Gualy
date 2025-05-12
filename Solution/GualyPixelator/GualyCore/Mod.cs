using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Mod : ArithmeticOperator
    {
        public Mod(CodeLocation location) : base(location)
        {
        }

        public override object Evaluate()
        {
            return LeftOperand.ArithmeticEvaluate() % RightOperand.ArithmeticEvaluate();
        }
    }
}
