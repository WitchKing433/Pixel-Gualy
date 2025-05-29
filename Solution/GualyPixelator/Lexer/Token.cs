using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    public class Token
    {
        public string Value { get; private set; }
        public TokenType Type { get; private set; }
        public (int,int) Location { get; private set; }                 // (Row, Column)
        public Token(TokenType type, string value, (int,int) location)
        {
            this.Type = type;
            this.Value = value;
            this.Location = location;
        }

        public override string ToString()
        {
            return $"[{Type}]-'{Value}'";
        }
    }


    public enum TokenType
    {
        Number,
        Instruction,
        Symbol,
        Text,
        Function
    }
}
