using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace GualyLexer
{
    public class GualyLexer
    {
        public void Run(string textCode)
        {    
            LexicalAnalyzer lexicalAnalyzer = Interpreter.GetLexicalAnalyzer();
            IEnumerable<Token> tokens = lexicalAnalyzer.GetTokens("code", textCode, new List<CompilingError>());

            foreach (Token token in tokens)
            {
                Console.WriteLine(token);
            }

            TokenStream tokenStream = new TokenStream(tokens);
            List<CompilingError> errors = new List<CompilingError>();
            Parser parser = new Parser(tokenStream, errors);

            

            Executer program = parser.ParseProgram(errors);

            Console.WriteLine(program);

            if (errors.Count > 0)
            {
                foreach (CompilingError error in errors)
                {
                    Console.WriteLine("{0}, {1}, {2}", error.Location.Line, error.Code, error.Argument);
                }
            }
        }




    }
}
