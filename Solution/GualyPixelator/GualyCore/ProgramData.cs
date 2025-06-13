using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public static class ProgramData
    {
        public static Dictionary<string, Type> instructions = new Dictionary<string, Type>
        {
            {"Color", typeof(ColorInstruction)},
            {"Size", typeof(SizeInstruction)},
            {"DrawLine", typeof(DrawLineInstruction)},
            {"DrawCircle", typeof(DrawCircleInstruction)},
            {"DrawRectangle", typeof(DrawRectangleInstruction)},
            {"Fill", typeof(FillInstruction)}
        };
        public static Dictionary<string, Type> functions = new Dictionary<string, Type>
        {
            {"GetActualX", typeof(GetActualX)},
            {"GetActualY", typeof(GetActualY)},
            {"GetCanvasSize", typeof(GetCanvasSize)},
            {"GetColorCount", typeof(GetColorCount)},
            {"IsBrushColor", typeof(IsBrushColor)},
            {"IsBrushSize", typeof(IsBrushSize)},
            {"IsCanvasColor", typeof(IsCanvasColor)}
        };
        public static Dictionary<string, Type> binaryOperators = new Dictionary<string, Type>
        {
            {"+", typeof(Add)},
            {"-", typeof(Sub)},
            {"*", typeof(Mul)},
            {"/", typeof(Div)},
            {"%", typeof(Mod)},
            {"**", typeof(Pow)},
            {"&&", typeof(And)},
            {"||", typeof(Or)},
            {"==", typeof(EqualComparator)},
            {"!=", typeof(NotEqualComparator)},
            {"<", typeof(LessComparator)},
            {">", typeof(GreaterComparator)},
            {"<=", typeof(LessEqualComparator)},
            {">=", typeof(GreaterEqualComparator)}
        };
        public static Dictionary<string, Type> unaryOperators = new Dictionary<string, Type>
        {
            {"!", typeof(Not)},
        };
        public static List<string> rightAssociative = new List<string>
        {
            "**",
            "!"
        };

        public static List<List<string>> listOperatorPrecedence = new List<List<string>>
        {
            new List<string>{"&&"},
            new List<string>{"||"},
            new List<string>{"==","!=",">","<",">=","<="},
            new List<string>{"+","-"},
            new List<string>{"*","/","%"},
            new List<string>{"**"},
            new List<string>{"!"}

        };
    }
}
