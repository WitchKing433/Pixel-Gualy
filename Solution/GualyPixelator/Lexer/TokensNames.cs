namespace Lexer
{
    public class TokensNames
    {
        public const string Add = "Addition"; // +
        public const string Sub = "Subtract"; // -
        public const string Mul = "Multiplication"; // *
        public const string Div = "Division"; // /
        public const string Pow = "Power"; // **
        public const string Mod = "Module"; // %

        public const string And = "And"; // &&
        public const string Or = "Or"; // ||
        public const string Not = "Not"; // !

        public const string Equal = "Equal"; // ==
        public const string Dif = "Diferent"; // !=
        public const string Greater = "Greater"; // >
        public const string GreaterEqual = "GreaterEqual"; // >=
        public const string Lower = "Lower"; // <
        public const string LowerEqual = "LowerEqual"; // <=

        public const string Assign = "Assign"; // <-
        public const string StatementSeparator = "StatementSeparator"; // \n
        public const string ParametersSeparator = "ParametersSeparator"; // ,

        public const string OpenBracket = "OpenBracket"; // (
        public const string ClosedBracket = "ClosedBracket"; // )
        public const string OpenSquareBracket = "OpenSquareBracket"; // [
        public const string ClosedSquareBracket = "ClosedSquareBracket"; // ]

        public const string True = "True";
        public const string False = "False";
        public const string GoTo = "GoTo";

        //Instructions

        public const string Spawn = "Spawn";
        public const string Color = "Color";
        public const string Size = "Size";
        public const string DrawLine = "DrawLine";
        public const string DrawCircle = "DrawCircle";
        public const string DrawRectangle = "DrawRectangle";
        public const string Fill = "Fill";

        //Functions

        public const string GetActualX = "GetActualX";
        public const string GetActualY = "GetActualY";
        public const string GetCanvasSize = "GetCanvasSize";
        public const string GetColorCount = "GetColorCount ";
        public const string IsBrushColor = "IsBrushColor";
        public const string IsBrushSize = "IsBrushSize";
        public const string IsCanvasColor = "IsCanvasColor";
    }
}
