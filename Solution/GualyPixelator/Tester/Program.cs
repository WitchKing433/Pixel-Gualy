using Lexer;
namespace Tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tokenizer tokenizer = new Tokenizer("$ \n dgd | &\nsf $");
            List<Token> tokens = tokenizer.Tokenize();
            for (int i = 0; i < tokens.Count; i++) 
            {
                Console.WriteLine(tokens[i].ToString());
            }
            for (int i = 0; i < tokenizer.errors.Count; i++)
            {
                Console.WriteLine( tokenizer.errors[i].ToString());
            }
        }
    }
}
