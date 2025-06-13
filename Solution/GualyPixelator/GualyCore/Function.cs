using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class Function : Expression
    {
        protected List<Expression> parameters;

        public Function(CodeLocation location, Type type, List<Expression> parameters) : base(location, type)
        {
            this.parameters = parameters;
        }
        public static Function BuildFunction(CodeLocation location, Type type, List<Expression> parameters, string function)
        {
            if(ProgramData.functions.TryGetValue(function, out Type value))
            {
                return (Function)Activator.CreateInstance(value, location, type, parameters);
            }
            throw new Exception("Invalid Function");                
        }
    }
}
