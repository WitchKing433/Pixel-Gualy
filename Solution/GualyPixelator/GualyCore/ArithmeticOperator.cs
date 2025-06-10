using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class ArithmeticOperator : ArithmeticExpression
    {
        protected ArithmeticExpression leftOperand;
        protected ArithmeticExpression rightOperand;

        protected ArithmeticOperator(CodeLocation location, Type type, ArithmeticExpression leftOperand, ArithmeticExpression rightOperand) : base(location, type)
        {
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }
    }
}
