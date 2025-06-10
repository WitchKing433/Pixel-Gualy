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

        public ProgramState Execute(ProgramState programState)
        {
            throw new NotImplementedException();
        }
    }
}
