using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Variable : Expression
    {
        public string Name {  get; private set; }
        Dictionary<string, ExpressionValue> variables;

        public Variable(string name, Dictionary<string,ExpressionValue> variables, CodeLocation location) : base(location)
        {
            Name = name;
            this.variables = variables;
        }

        public override object Evaluate()
        {
            if(variables.TryGetValue(Name, out var value))
                return value.Evaluate();
            throw new Exception($"Missing variable '{Name}'");
        }
    }
}
