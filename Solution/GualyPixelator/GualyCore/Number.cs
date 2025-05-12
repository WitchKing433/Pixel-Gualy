using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Number : ArithmeticExpression
    {
        public ExpressionValue Value { get; private set; }

        public Number(int value, CodeLocation location) : base(location)
        {
            Value = new ExpressionValue(value);
        }

        public override object Evaluate()
        {
            return Value.Evaluate();
        }
    }
}
