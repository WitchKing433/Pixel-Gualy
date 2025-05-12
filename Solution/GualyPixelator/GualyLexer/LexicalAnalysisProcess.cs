
using GualyCore;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace GualyLexer
{


    public class LexicalAnalyzer
    {
        Dictionary<string, string> operators = new Dictionary<string, string>();
        Dictionary<string, string> keywords = new Dictionary<string, string>();
        Dictionary<string, string> functions = new Dictionary<string, string>();
        Dictionary<string, string> symbols = new Dictionary<string, string>();

        public IEnumerable<string> Keywords { get { return keywords.Keys; } }

        /* Associates an operator symbol with the correspondent token value */
        public void RegisterOperator(string operatorName, string tokenName)
        {
            this.operators[operatorName] = tokenName;
        }

        /* Associates a keyword with the correspondent token value */
        public void RegisterKeyword(string keyword, string tokenName)
        {
            this.keywords[keyword] = tokenName;
        }
        public void RegisterSymbol(string symbol, string tokenName)
        {
            this.symbols[symbol] = tokenName;
        }
        public void RegisterFunction(string function, string tokenName)
        {
            this.functions[function] = tokenName;
        }

        /* Matches a new symbol in the code and read it from the string. The new symbol is added to the token list as an operator. */
        private bool MatchSymbol(TokenReader tokenReader, List<Token> tokens)
        {
            foreach (var operatorName in operators.Keys.OrderByDescending(k => k.Length))
                if (tokenReader.Match(operatorName))
                {
                    tokens.Add(new Token(TokenType.Symbol, operators[operatorName], tokenReader.Location));
                    return true;
                }
            return false;
        }

        /* Returns all tokens read from the code and populate the errors list with all lexical errors detected. */
        public IEnumerable<Token> GetTokens(string fileName, string textCode, List<CompilingError> errors)
        {
            List<Token> tokens = new List<Token>();

            TokenReader tokenReader = new TokenReader(fileName, textCode);

            while (!tokenReader.EOF)
            {
                string value;

                if (tokenReader.ReadWhiteSpace())
                    continue;

                if (tokenReader.ReadIdentifier(out value))
                {
                    if (keywords.ContainsKey(value))
                        tokens.Add(new Token(TokenType.Keyword, keywords[value], tokenReader.Location));
                    else
                        tokens.Add(new Token(TokenType.Identifier, value, tokenReader.Location));
                    continue;
                }

                if (tokenReader.ReadNumber(out value))
                {
                    int d;
                    if (!int.TryParse(value, out d))
                        errors.Add(new CompilingError(tokenReader.Location, ErrorCode.Invalid, "Number format"));
                    tokens.Add(new Token(TokenType.Number, value, tokenReader.Location));
                    continue;
                }

                if (MatchSymbol(tokenReader, tokens))
                    continue;

                var unkOp = tokenReader.ReadAny();
                errors.Add(new CompilingError(tokenReader.Location, ErrorCode.Unknown, unkOp.ToString()));
            }

            return tokens;
        }

        /* Allows to read from a string numbers, identifiers and matching some prefix. 
        It has some useful methods to do that */
        class TokenReader
        {
            string FileName;
            string textCode;
            int posBegin;
            int posEnd;
            int line;
            int lastLB;

            public TokenReader(string fileName, string textCode)
            {
                this.FileName = fileName;
                this.textCode = textCode;
                this.posBegin = 0;
                this.posEnd = 0;
                this.line = 1;
                this.lastLB = -1;
            }

            public CodeLocation Location
            {
                get
                {
                    return new CodeLocation
                    {
                        File = FileName,
                        Line = line,
                        Begin = posBegin - lastLB,
                        End = posEnd - lastLB
                    };
                }
            }

            /* Peek the next character */
            public char Peek()
            {
                if (posBegin < 0 || posBegin >= textCode.Length)
                    throw new InvalidOperationException();

                return textCode[posBegin];
            }

            public bool EOF
            {
                get { return posBegin >= textCode.Length; }
            }

            public bool EOL
            {
                get { return EOF || textCode[posBegin] == '\n'; }
            }

            public bool ContinuesWith(string prefix)
            {
                if (posBegin + prefix.Length > textCode.Length)
                    return false;
                for (int i = 0; i < prefix.Length; i++)
                    if (textCode[posBegin + i] != prefix[i])
                        return false;
                return true;
            }

            public bool Match(string prefix)
            {
                if (ContinuesWith(prefix))
                {
                    posBegin += prefix.Length;
                    return true;
                }

                return false;
            }

            public bool ValidIdCharacter(char c, bool begining)
            {
                return (begining ? char.IsLetter(c) : (char.IsLetterOrDigit(c) || c == '-'));
            }

            public bool ReadIdentifier(out string identifier)
            {
                identifier = "";
                while (!EOL && ValidIdCharacter(Peek(), identifier.Length == 0))
                    identifier += ReadAny();
                return identifier.Length > 0 && identifier[identifier.Length - 1] != '-';
            }

            public bool ReadNumber(out string number)
            {
                number = "";
                while (!EOL && char.IsDigit(Peek()))
                    number += ReadAny();
                return number.Length > 0;
            }

            public bool ReadUntil(string end, out string text)
            {
                text = "";
                while (!Match(end))
                {
                    if (EOL || EOF)
                        return false;
                    text += ReadAny();
                }
                return true;
            }

            public bool ReadWhiteSpace()
            {
                if (char.IsWhiteSpace(Peek()))
                {
                    ReadAny();
                    return true;
                }
                return false;
            }

            public char ReadAny()
            {
                if (EOF)
                    throw new InvalidOperationException();

                if (EOL)
                {
                    line++;
                    lastLB = posBegin;
                }
                return textCode[posBegin++];
            }
        }

    }
}

