using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Number : ArithmeticExpression
    {
        public int Value {  get; private set; }
        public Number(CodeLocation location, Type type, int value) : base(location, type)
        {
            Value = value;
        }

        public override object Evaluate(ProgramState programState)
        {
            return Value;
        }
    }
}
