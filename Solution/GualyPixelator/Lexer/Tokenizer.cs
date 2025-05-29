using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lexer
{
    public class Tokenizer
    {
        Dictionary<string, string> operators = new Dictionary<string, string>();
        Dictionary<string, string> instructions = new Dictionary<string, string>();
        Dictionary<string, string> functions = new Dictionary<string, string>();
        Dictionary<string, string> symbols = new Dictionary<string, string>();
        List<string> operatorsParts = new List<string>();
        delegate bool Predicate(string s);
        int row = 1;
        int col = 1;
        int i = 0;
        string code;
        public List<Error> errors;

        public Tokenizer(string code)
        {
            this.code = code;
            errors = new List<Error>();
        }

        public List<Token> Tokenize()
        {
            RegisterAll();
            List<Token> tokens = new List<Token>();
            string text = "";

            for (; i < code.Length; i++, col++)
            {
                text = "";
                if (code[i] == ' ')
                    continue;
                if (code[i] == '\n')
                {
                    tokens.Add(new Token(TokenType.Symbol, "\n",(row,col)));
                    col = 0;
                    row++;
                    continue;
                }
                if (MatchNumber(code[i].ToString()))
                {
                    tokens.Add(MatchString(MatchNumber, TokenType.Number));
                    continue;
                }
                if (MatchWord(code[i].ToString()))
                {
                    tokens.Add(MatchString(MatchWord, TokenType.Text));
                    continue;
                }
                if (MatchOperator(code[i].ToString()))
                {
                    tokens.Add(MatchString(MatchOperator, TokenType.Symbol));
                    continue;
                }

            }
            
            return tokens;
        }













        Token MatchString (Predicate predicate, TokenType type)
        {
            string text = "";
            for (; i < code.Length && predicate(text + code[i]); i++, col++)
            {
                text += code[i];
            }
            Token token = new Token(type, text, (row, col));
            i--;
            col--;
            return token;
        }
        bool MatchNumber(string text) 
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(text);
        }
        bool MatchWord(string word)
        {
            Regex regex = new Regex(@"^");
            return regex.IsMatch(word);
        }
        bool MatchOperator(string op)
        {
            if (operatorsParts.Contains(op))
                return true;
            if (operators.ContainsKey(op))
                return true;
            return false;
        }

        void RegisterAll()
        {
            operatorsParts.Add("&");
            operatorsParts.Add("|");
            operatorsParts.Add("=");

            RegisterOperator("+", TokensNames.Add);
            RegisterOperator("*", TokensNames.Mul);
            RegisterOperator("-", TokensNames.Sub);
            RegisterOperator("/", TokensNames.Div);
            RegisterOperator("**", TokensNames.Pow);
            RegisterOperator("%", TokensNames.Mod);

            RegisterOperator("&&", TokensNames.And);
            RegisterOperator("||", TokensNames.Or);
            RegisterOperator("!", TokensNames.Not);

            RegisterOperator("==", TokensNames.Equal);
            RegisterOperator("!=", TokensNames.Dif);
            RegisterOperator(">", TokensNames.Greater);
            RegisterOperator(">=", TokensNames.GreaterEqual);
            RegisterOperator("<", TokensNames.Lower);
            RegisterOperator("<=", TokensNames.LowerEqual);

            RegisterOperator("<-", TokensNames.Assign);
            RegisterSymbol("\n", TokensNames.StatementSeparator);
            RegisterSymbol(",", TokensNames.ParametersSeparator);

            RegisterOperator("(", TokensNames.OpenBracket);
            RegisterOperator(")", TokensNames.ClosedBracket);
            RegisterSymbol("[", TokensNames.OpenSquareBracket);
            RegisterSymbol("]", TokensNames.ClosedSquareBracket);

            RegisterInstruction("GoTo", TokensNames.GoTo);

            RegisterInstruction("Spawn", TokensNames.Spawn);
            RegisterInstruction("Color", TokensNames.Color);
            RegisterInstruction("Size", TokensNames.Size);
            RegisterInstruction("DrawLine", TokensNames.DrawLine);
            RegisterInstruction("DrawCircle", TokensNames.DrawCircle);
            RegisterInstruction("DrawRectangle", TokensNames.DrawRectangle);
            RegisterInstruction("Fill", TokensNames.Fill);

            RegisterFunction("GetActualX", TokensNames.GetActualX);
            RegisterFunction("GetActualY", TokensNames.GetActualY);
            RegisterFunction("GetCanvasSize", TokensNames.GetCanvasSize);
            RegisterFunction("GetColorCount", TokensNames.GetColorCount);
            RegisterFunction("IsBrushColor", TokensNames.IsBrushColor);
            RegisterFunction("IsBrushSize", TokensNames.IsBrushSize);
            RegisterFunction("IsCanvasColor", TokensNames.IsCanvasColor);
        }
        void RegisterOperator(string oper, string name)
        {
            operators[oper] = name;
        }
        void RegisterSymbol(string symbol, string name)
        {
            operators[symbol] = name;
        }
        void RegisterInstruction(string instr, string name)
        {
            operators[instr] = name;
        }
        void RegisterFunction(string funct, string name)
        {
            operators[funct] = name;
        }
    }
}
