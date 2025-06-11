using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Variable : ArithmeticExpression
    {
        public string Name {  get; private set; }

        public Variable(string name, CodeLocation location,Type type) : base(location, type)
        {
            Name = name;
        }

        public override object Evaluate(ProgramState programState)
        {
            return programState.variables[Name];
        }
    }
}
