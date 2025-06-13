using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class And : BinaryOperator
    {
        public And(CodeLocation location, Type type, ArithmeticExpression leftOperand, ArithmeticExpression rightOperand) : base(location, type, leftOperand, rightOperand)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            return leftOperand.ArithmeticEvaluate(programState) != 0 && rightOperand.ArithmeticEvaluate(programState) != 0 ? 1 : 0;
                
        }
    }
}
