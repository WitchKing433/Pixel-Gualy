using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Div : BinaryOperator
    {
        public Div(CodeLocation location, Type type, ArithmeticExpression leftOperand, ArithmeticExpression rightOperand) : base(location, type, leftOperand, rightOperand)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            int left = leftOperand.ArithmeticEvaluate(programState);
            int right = rightOperand.ArithmeticEvaluate(programState);
            if (right == 0)
                throw new Exception("Invalid expression, cannot divide by 0");
            return left / right;
        }
    }
}
