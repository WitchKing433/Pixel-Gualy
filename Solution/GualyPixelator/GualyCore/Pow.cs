using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Pow : BinaryOperator
    {
        public Pow(CodeLocation location, Type type, ArithmeticExpression leftOperand, ArithmeticExpression rightOperand) : base(location, type, leftOperand, rightOperand)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            return (int)Math.Pow(leftOperand.ArithmeticEvaluate(programState), rightOperand.ArithmeticEvaluate(programState));
        }
    }
}
