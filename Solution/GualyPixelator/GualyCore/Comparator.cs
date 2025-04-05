using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class Comparator : LogicExpression
    {
        protected ArithmeticExpression leftOperand;
        protected ArithmeticExpression rightOperand;
    }
}
