using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class AssignmentExpression : ILineNode
    {
        string name;
        Expression expression;
        CodeLocation location;

        public AssignmentExpression(string name, Expression expression, CodeLocation location)
        {
            this.name = name;
            this.expression = expression;
            this.location = location;
        }

        public ProgramState Execute(ProgramState programState)
        {
            programState.variables[name] = expression.Evaluate(programState);
            return programState;
        }
    }
}
