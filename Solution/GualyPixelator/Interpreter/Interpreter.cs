using Lexer;
using Parser;
using GualyCore;

namespace Interpreter
{
    public class Interpreter
    {
        string code;
        int size;
        int index = 0;
        string folderPath;
        string fullPath;
        List<Error> errors = new List<Error>();
        public Interpreter(string code, int size)
        {
            this.size = size; 
            this.code = code.Replace("\r", "");
            code.Replace("\r", "");
            folderPath = AppDomain.CurrentDomain.BaseDirectory;
            fullPath = Path.Combine(folderPath, "ErrorsLog.txt");
        }
        public bool PaintAll()
        {
            string sErrors = "";
            index = 0;
            Tokenizer tokenizer = new Tokenizer(code);
            List<Token> tokens = tokenizer.Tokenize();
            errors = tokenizer.errors;
            if (errors.Count == 0)
            {
                Parser.Parser parser = new Parser.Parser(tokens);
                List<ILineNode> program = parser.Parse();
                errors = parser.errors;
                if (errors.Count == 0)
                {
                    ProgramState programState = new ProgramState((size,size), parser.labels);
                    try
                    {
                        int iterations = 0;
                        for (; programState.index < program.Count; programState.index++, iterations++)
                        {
                            program[programState.index].Execute(programState);
                            if (iterations == int.MaxValue)
                                throw new Exception("Infinite loop");
                        }
                        programState.CreateImage();
                        return true;
                    }
                    catch(Exception exc)
                    {
                        sErrors += exc.Message;
                    }
                }
            }
            sErrors += errorsToString();
            File.WriteAllText(fullPath, sErrors);
            return false;
        }
        string errorsToString()
        {
            string sErrors = "";
            for (int i = 0; i < errors.Count; i++)
            {
                sErrors += (errors[i].ToString() + '\n');
            }
            return sErrors;
        }
    }
}
