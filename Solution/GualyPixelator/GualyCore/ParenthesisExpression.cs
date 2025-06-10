using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class ParenthesisExpression : Expression
    {
        public Expression Expression { get; private set; }
        public ParenthesisExpression(CodeLocation location, Type type, Expression expression) : base(location, type)
        {
            this.Expression = expression;
        }

        public override object Evaluate(ProgramState programState)
        {
            return Expression.Evaluate(programState);
        }
    }
}
