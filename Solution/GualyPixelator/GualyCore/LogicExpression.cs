using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class LogicExpression : Expression
    {
        protected LogicExpression(CodeLocation location) : base(location)
        {
            type = typeof(bool);
        }
    }
}
