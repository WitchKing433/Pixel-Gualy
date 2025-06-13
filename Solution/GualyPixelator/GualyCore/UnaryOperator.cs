using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class UnaryOperator : ArithmeticExpression
    {
        protected ArithmeticExpression rightOperand;
        public UnaryOperator(CodeLocation location, Type type, ArithmeticExpression rightOperand) : base(location, type)
        {
            this.rightOperand = rightOperand;
        }

        public static UnaryOperator BuildOperator(string oper ,CodeLocation location, ArithmeticExpression rightExpression) 
        {
            if(ProgramData.unaryOperators.TryGetValue(oper, out Type value))
                return (UnaryOperator)Activator.CreateInstance(value, location, typeof(int), rightExpression);
            throw new Exception("Invalid Operator");
        }
    }
}
