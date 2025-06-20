using Lexer;
using Parser;
using GualyCore;

namespace Interpreter
{
    public class Interpreter
    {
        public int Index { get => programState.index; }
        Dictionary<string, int> labels;
        List<ILineNode> program;
        ProgramState programState;
        public Interpreter() { }

        public string Compile(string code, string outputPath, string fileName)
        {
            code = code.Replace("\r", "");
            string sErrors = "";
            Tokenizer tokenizer = new Tokenizer(code);
            List<Token> tokens = tokenizer.Tokenize();
            List<Error> errors = tokenizer.errors;
            if (errors.Count == 0)
            {
                Parser.Parser parser = new Parser.Parser(tokens);
                labels = parser.labels;
                program = parser.Parse();
                errors = parser.errors;
                if (errors.Count == 0)
                {
                    return "";
                }
            }
            sErrors += errorsToString(errors);
            string outputPathFileName = Path.Combine(outputPath, Path.GetFileName(fileName) + ".err");
            File.WriteAllText(outputPathFileName, sErrors);
            return outputPathFileName;
        }
        public string PaintAll(int size, string outputPath, string fileName)
        {
            try
            {
                if (size < 1)
                    throw new Exception("Invalid canvas size");
                if (program == null)
                    throw new Exception("Cant paint without a compiled program");
                ProgramState programState = new ProgramState((size, size), labels);

                int iterations = 0;
                for (; programState.index < program.Count; programState.index++, iterations++)
                {
                    program[programState.index].Execute(programState);
                    if (iterations == int.MaxValue)
                        throw new Exception("Infinite loop");
                }
                return programState.CreateImage(outputPath, fileName);
            }
            catch (Exception exc)
            {
                string outputPathFileName = Path.Combine(outputPath, Path.GetFileName(fileName) + ".err");
                File.WriteAllText(outputPathFileName, exc.Message);
                return outputPathFileName;
            }

        }
        public string PaintBegin(int size, string outputPath, string fileName)
        {
            try
            {
                if (size < 1)
                    throw new Exception("Invalid canvas size");
                if (program == null)
                    throw new Exception("Cant paint without a compiled program");
                programState = new ProgramState((size, size), labels);
                return "";
            }
            catch (Exception exc)
            {
                string outputPathFileName = Path.Combine(outputPath, Path.GetFileName(fileName) + ".err");
                File.WriteAllText(outputPathFileName, exc.Message);
                return outputPathFileName;
            }

        }
        public string PaintCurrent(string outputPath, string fileName)
        {
            try
            {
                if (programState != null && programState.index < program.Count)
                {
                    program[programState.index].Execute(programState);
                    programState.index++;
                    return programState.CreateImage(outputPath, fileName);
                }
                throw new Exception("Program execution ended");
            }
            catch (Exception exc)
            {
                string outputPathFileName = Path.Combine(outputPath, Path.GetFileName(fileName) + ".err");
                File.WriteAllText(outputPathFileName, exc.Message);
                return outputPathFileName;
            }
        }
        string errorsToString(List<Error> errors)
        {
            string sErrors = "";
            for (int i = 0; i < errors.Count; i++)
            {
                sErrors += (errors[i].ToString() + '\n');
            }
            return sErrors;
        }
        public static string ImageToCode(string path, int imageMaxSize)
        {
            return ProgramState.ImageToCode(path, imageMaxSize);
        }
    }
}
