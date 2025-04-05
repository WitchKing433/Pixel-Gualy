using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class LogicOperator : LogicExpression
    {
        protected LogicExpression leftOperand;
        protected LogicExpression rightOperand;
    }
}
