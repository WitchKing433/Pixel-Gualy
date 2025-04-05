using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class LogicExpression : IExpression
    {
        public abstract object Evaluate();

        public bool LogicEvaluate()
        {
            return (bool)Evaluate();
        }   
    }
}
