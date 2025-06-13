using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Not : UnaryOperator
    {
        public Not(CodeLocation location, Type type, ArithmeticExpression rightOperand) : base(location, type, rightOperand)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            return rightOperand.ArithmeticEvaluate(programState) == 0 ? 1 : 0;
        }
    }
}
