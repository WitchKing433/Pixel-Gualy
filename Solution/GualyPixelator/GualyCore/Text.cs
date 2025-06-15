using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Text : Expression
    {
        public string Value {  get; private set; }
        public Text(CodeLocation location, Type type, string text) : base(location, type)
        {
            Value = text;
        }

        public override object Evaluate(ProgramState programState)
        {
            return Value;
        }
    }
}
