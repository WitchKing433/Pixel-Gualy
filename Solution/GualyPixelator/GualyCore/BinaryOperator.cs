using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class BinaryOperator : ArithmeticExpression
    {
        protected ArithmeticExpression leftOperand;
        protected ArithmeticExpression rightOperand;

        protected BinaryOperator(CodeLocation location, Type type, ArithmeticExpression leftOperand, ArithmeticExpression rightOperand) : base(location, type)
        {
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }

        public static BinaryOperator BuildOperator(CodeLocation location, ArithmeticExpression leftExpression, ArithmeticExpression rightExpression,  string oper)
        {

            if(ProgramData.binaryOperators.TryGetValue(oper,out Type value))
            {
                return (BinaryOperator)Activator.CreateInstance(value, location, typeof(int), leftExpression, rightExpression);
            }
            throw new Exception("Invalid Operator");
        }
    }
}
