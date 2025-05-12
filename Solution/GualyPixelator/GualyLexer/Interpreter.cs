using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyLexer
{
    public class Interpreter
    {
        private static LexicalAnalyzer? lexicalProcess;
        public static LexicalAnalyzer GetLexicalAnalyzer()
        {
            if (lexicalProcess == null)
            {
                lexicalProcess = new LexicalAnalyzer();


                lexicalProcess.RegisterOperator("+", TokensNames.Add);
                lexicalProcess.RegisterOperator("*", TokensNames.Mul);
                lexicalProcess.RegisterOperator("-", TokensNames.Sub);
                lexicalProcess.RegisterOperator("/", TokensNames.Div);
                lexicalProcess.RegisterOperator("**", TokensNames.Pow);
                lexicalProcess.RegisterOperator("%", TokensNames.Mod);

                lexicalProcess.RegisterOperator("&&", TokensNames.And);
                lexicalProcess.RegisterOperator("||", TokensNames.Or);
                lexicalProcess.RegisterOperator("!", TokensNames.Not);

                lexicalProcess.RegisterOperator("==", TokensNames.Equal);
                lexicalProcess.RegisterOperator("!=", TokensNames.Dif);
                lexicalProcess.RegisterOperator(">", TokensNames.Greater);
                lexicalProcess.RegisterOperator(">=", TokensNames.GreaterEqual);
                lexicalProcess.RegisterOperator("<", TokensNames.Lower);
                lexicalProcess.RegisterOperator("<=", TokensNames.LowerEqual);

                lexicalProcess.RegisterOperator("<-", TokensNames.Assign);
                lexicalProcess.RegisterSymbol("\n", TokensNames.StatementSeparator);
                lexicalProcess.RegisterSymbol(",", TokensNames.ParametersSeparator);

                lexicalProcess.RegisterOperator("(", TokensNames.OpenBracket);
                lexicalProcess.RegisterOperator(")", TokensNames.ClosedBracket);
                lexicalProcess.RegisterSymbol("[", TokensNames.OpenSquareBracket);
                lexicalProcess.RegisterSymbol("]", TokensNames.ClosedSquareBracket);

                lexicalProcess.RegisterKeyword("True", TokensNames.True);
                lexicalProcess.RegisterKeyword("False", TokensNames.False);
                lexicalProcess.RegisterKeyword("GoTo", TokensNames.GoTo);

                lexicalProcess.RegisterKeyword("Spawn", TokensNames.Spawn);
                lexicalProcess.RegisterKeyword("Color", TokensNames.Color);
                lexicalProcess.RegisterKeyword("Size", TokensNames.Size);
                lexicalProcess.RegisterKeyword("DrawLine", TokensNames.DrawLine);
                lexicalProcess.RegisterKeyword("DrawCircle", TokensNames.DrawCircle);
                lexicalProcess.RegisterKeyword("DrawRectangle", TokensNames.DrawRectangle);
                lexicalProcess.RegisterKeyword("Fill", TokensNames.Fill);

                lexicalProcess.RegisterFunction("GetActualX", TokensNames.GetActualX);
                lexicalProcess.RegisterFunction("GetActualY", TokensNames.GetActualY);
                lexicalProcess.RegisterFunction("GetCanvasSize", TokensNames.GetCanvasSize);
                lexicalProcess.RegisterFunction("GetColorCount", TokensNames.GetColorCount);
                lexicalProcess.RegisterFunction("IsBrushColor", TokensNames.IsBrushColor);
                lexicalProcess.RegisterFunction("IsBrushSize", TokensNames.IsBrushSize);
                lexicalProcess.RegisterFunction("IsCanvasColor", TokensNames.IsCanvasColor);
            }

            return lexicalProcess;

        }
    }
}
