using Lexer;
using GualyCore;
using Parser;
using System.Drawing;
namespace Tester
{
    public class Program
    {
        static void Main(string[] args)
        {
            Tokenizer tokenizer = new Tokenizer("Spawn(2&&3,1+2)\n");
            List<Token> tokens = tokenizer.Tokenize();
            for (int i = 0; i < tokens.Count; i++) 
            {
                Console.WriteLine(tokens[i].ToString());
            }
            for (int i = 0; i < tokenizer.errors.Count; i++)
            {
                Console.WriteLine( tokenizer.errors[i].ToString());
            }
            Parser.Parser parser = new Parser.Parser(tokens);
            List<ILineNode> program = parser.Parse();
            Color[,] map = new Color[10, 600];
            ProgramState programState = new ProgramState(0, map , 1, new System.Drawing.Color(),(0,0));
            if (program != null)
            {
                Console.WriteLine("siiiiiiiii");
                program[0].Execute(programState);
                Console.WriteLine($"({programState.wallePosition.Item2},{programState.wallePosition.Item1})");
            }
            else
                Console.WriteLine("nooooooooo");
        }
    }
}
