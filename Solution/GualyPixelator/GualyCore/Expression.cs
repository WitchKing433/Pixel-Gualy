using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class Expression : IExpression
    {
        public CodeLocation Location { get; set; }
        protected Type type;
        public Type ExprType { get { return type; } private set { } }

        public Expression(CodeLocation location)
        {
            Location = location;
        }
        public abstract object Evaluate();
    }
}
