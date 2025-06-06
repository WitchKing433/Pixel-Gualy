using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class ExpressionValue : Expression
    {
        public object Value {  get; private set; }

        public CodeLocation Location { get; set; }

        public ExpressionValue(object value)
        {
            Value = value;
        }

        public object Evaluate()
        {
            return Value;
        }
    }
}
