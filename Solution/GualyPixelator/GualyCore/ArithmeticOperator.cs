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

        public static ArithmeticOperator BuildOperator(CodeLocation location, ArithmeticExpression leftExpression, ArithmeticExpression rightExpression,  string oper)
        {
            switch (oper)
            {
                case "+": { return new Add(location, typeof(int), leftExpression, rightExpression); }

                case "-": { return new Sub(location, typeof(int), leftExpression, rightExpression); }

                case "/": { return new Div(location, typeof(int), leftExpression, rightExpression); }

                case "*": { return new Mul(location, typeof(int), leftExpression, rightExpression); }

                case "%": { return new Mod(location, typeof(int), leftExpression, rightExpression); }

                case "**": { return new Pow(location, typeof(int), leftExpression, rightExpression); }

                default: { throw new Exception("Invalid Operator"); }
            }
        }
    }
}
