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
                else if (code[i] == '\n')
                {
                    tokens.Add(new Token(TokenType.Symbol, "\n",(row,col)));
                    col = 0;
                    row++;
                    continue;
                }
                else if (MatchNumber(code[i].ToString()))
                {
                    tokens.Add(MatchString(MatchNumber, TokenType.Number));
                    continue;
                }
                else if (MatchWord(code[i].ToString()))
                {
                    tokens.Add(MatchString(MatchWord, TokenType.Text));
                    continue;
                }
                else if (MatchOperator(code[i].ToString()))
                {
                    Token op = MatchString(MatchOperator, TokenType.Operator);
                    if (operators.ContainsKey(op.Value))
                    {
                        tokens.Add(op);
                    }
                    else
                        errors.Add(new Error("Invalid operator", op.Location));
                    continue;
                }
                else if (MatchSymbol(code[i].ToString()))
                {
                    tokens.Add(MatchString(MatchSymbol, TokenType.Symbol));
                }
                else
                {
                    errors.Add(new Error("Syntax error", (row, col)));
                }

            }
            
            return tokens;
        }













        Token MatchString (Predicate predicate, TokenType type)
        {
            string text = "";
            for (; i < code.Length && code[i] != '\n' && predicate(text + code[i]); i++, col++)
            {
                text += code[i];
            }
            if(type == TokenType.Text)
            {
                if(MatchFunction(text))
                    type = TokenType.Function;
                else if(MatchInstruction(text))
                    type = TokenType.Instruction;
            }
            Token token = new Token(type, text, (row, col - text.Length));
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
            Regex regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9_]*$");
            return regex.IsMatch(word);
        }
        bool MatchOperator(string op)
        {
            return operatorsParts.Contains(op) || operators.ContainsKey(op);
        }
        bool MatchFunction(string text)
        {
            return functions.ContainsKey(text);
        }
        bool MatchInstruction(string text)
        {
            return instructions.ContainsKey(text);
        }
        bool MatchSymbol(string symbol)
        {
            return symbols.ContainsKey(symbol);
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
            RegisterSymbol("\"", TokensNames.DoubleQuotes);

            RegisterSymbol("(", TokensNames.OpenBracket);
            RegisterSymbol(")", TokensNames.ClosedBracket);
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
            symbols[symbol] = name;
        }
        void RegisterInstruction(string instr, string name)
        {
            instructions[instr] = name;
        }
        void RegisterFunction(string funct, string name)
        {
            functions[funct] = name;
        }
    }
}
