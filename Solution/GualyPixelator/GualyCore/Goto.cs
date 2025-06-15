using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class GoTo : ILineNode
    {
        string destinLabel;
        Expression expression;

        public GoTo(string label, CodeLocation location, Expression expression) 
        {
            destinLabel = label;
            this.expression = expression;
        }

        public void Execute(ProgramState programState)
        {
            if (programState.labels.TryGetValue(destinLabel, out int value))
            {
                if((int)expression.Evaluate(programState) != 0)
                {
                    programState.index = value;
                }
            }
            else
            {
                throw new Exception("Missing label");
            }
        }
    }
}
