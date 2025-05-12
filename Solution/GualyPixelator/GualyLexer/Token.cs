using GualyCore;

namespace GualyLexer
{
    public class Token
    {
        public string Name { get; private set; }
        public TokenType Type { get; private set; }
        public CodeLocation Location { get; private set; }
        public Token(TokenType type, string value, CodeLocation location)
        {
            this.Type = type;
            this.Name = value;
            this.Location = location;
        }

        public override string ToString()
        {
            return $"[{Type}]-'{Name}'";
        }
    }


    public enum TokenType
    {
        Unknown,
        Number,
        Keyword,
        Identifier,
        Symbol,
        Text,
        Function
    }
}
