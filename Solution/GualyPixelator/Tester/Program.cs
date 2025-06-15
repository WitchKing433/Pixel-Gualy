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
            Tokenizer tokenizer = new Tokenizer("Spawn (500,500)\nColor(\" Gold\")\nSize(50)\nDrawRectangle(0,0,0,250,400) \nColor(\" Silver\")\n DrawLine(1,1,0)\nColor(\"Transparent\")\nDrawLine(1,0,100)\nColor(\"DarkBlue\")\n Fill()\n");
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
            ProgramState programState = new ProgramState((1000,1000), parser.labels);
            for (int i = 0; i < parser.errors.Count; i++)
            {
                Console.WriteLine(parser.errors[i].ToString());
            }
            if (program != null)
            {
                for (int i = 0; i < program.Count; i++) 
                {
                    program[i].Execute(programState);
                }
                Console.WriteLine("siiiiiiiii");
                //Console.WriteLine($"({programState.wallePosition.Item1},{programState.wallePosition.Item2})");
                //Console.WriteLine(programState.variables["nio"]);
                //Console.WriteLine(programState.brushColor);
                //Console.WriteLine(programState.brushSize);
                programState.CreateImage();
            }
            else
                Console.WriteLine("nooooooooo");
        }
    }
}
