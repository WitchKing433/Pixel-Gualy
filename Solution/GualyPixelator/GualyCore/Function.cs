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
            switch (function)
            {
                case "GetActualX": { return new GetActualX(location, type, parameters); }

                case "GetActualY": { return new GetActualY(location, type, parameters); }

                case "GetCanvasSize": { return new GetCanvasSize(location, type, parameters); }

                case "GetColorCount": { return new GetColorCount(location, type, parameters); }

                case "IsBrushColor": { return new IsBrushColor(location, type, parameters); }

                case "IsBrushSize": { return new IsBrushSize(location, type, parameters); }

                case "IsCanvasColor": { return new IsCanvasColor(location, type, parameters); }

                default: { throw new Exception("Invalid Function"); }
                    
            }
        }
    }
}
