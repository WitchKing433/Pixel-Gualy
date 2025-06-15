using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GualyCore
{
    public class GetColorCount : Function
    {
        public GetColorCount(CodeLocation location, Type type, List<Expression> parameters) : base(location, type, parameters)
        {
        }

        public override object Evaluate(ProgramState programState)
        {
            try
            {
                int x1 = (int)parameters[1].Evaluate(programState);
                int y1 = (int)parameters[2].Evaluate(programState);
                int x2 = (int)parameters[3].Evaluate(programState);
                int y2 = (int)parameters[4].Evaluate(programState);
                Color color1 = programState.canvas[x1, y1];
                Color color2 = programState.canvas[x2, y2];
                string colorName = parameters[0].Evaluate(programState).ToString();
                int colorCount = 0;
                for (int i = Math.Min(x1, x2); i <= Math.Max(x1, x2); i++)
                {
                    for (int j = Math.Min(y1, y2); j <= Math.Max(y1, y2); j++)
                    {
                        if(programState.EqualColor(colorName, programState.canvas[i, j]))
                        {
                            colorCount++;
                        }
                    }
                }
                return colorCount;
            }
            catch
            {
                return 0;
            }
        }
    }
}
