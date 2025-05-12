using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class ArithmeticOperator : ArithmeticExpression
    {
        public ArithmeticExpression LeftOperand {  get; set; }
        public ArithmeticExpression RightOperand {  get; set; }

        protected ArithmeticOperator(CodeLocation location) : base(location)
        {
        }
    }
}
