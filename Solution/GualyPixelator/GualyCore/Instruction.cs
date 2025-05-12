using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public abstract class Instruction : ILineNode
    {
        public CodeLocation location;
        public List<IExpression> parameters = new List<IExpression>();

        public Instruction(CodeLocation location)
        {
            this.location = location;
        }

        public void AddParameter(IExpression expression)
        {
            parameters.Add(expression);
        }

        public abstract Color[,] ChangePixelMap(Color[,] previusPixelMap);

    }
}
