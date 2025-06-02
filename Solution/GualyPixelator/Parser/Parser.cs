using Lexer;
using GualyCore;
namespace Parser
{
    public class Parser
    {
        Dictionary<string, List<Type>> admissibleParameters = new Dictionary<string,List<Type>>();
        List<Token> tokens;
        List<Error> errors;
        List<ILineNode> program;
        public List<Error> Errors {  get { return errors; } private set { } }
        int i = 0;
        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            errors = new List<Error>();
            RegisterParameters();
        }

        public List<ILineNode> Parse()
        {
            i = 0;
            for (; i < tokens.Count; i++)
            {

            }
        }

        bool ParseSpawn()
        {
            if (tokens[0].Value == "Spawn")
            {
                (int, int) location = tokens[0].Location;
                i++;
                List<IExpression> parameters = ParseParameters();
                if (ValidateParameters(parameters, "int,int"))
                {
                    if (i + 1 < tokens.Count && (tokens[i + 1].Value == "\n"))
                        program.Add(new Spawn(BuildLocation(location),parameters));
                    else
                    {
                        AddError("EndOfLine expected");
                        return false;
                    }
                }
                else
                {
                    AddError("Invalid parameters");
                    return false;
                }
            }
            else
            {
                AddError("Spawn spected");
                return false;
            }
            return true;
        }
        List<IExpression> ParseParameters()
        {
            List<IExpression> parameters = new List<IExpression>();
            if (tokens[i].Value != "(")
            {
                AddError("( expected");
            }
            else
                i++;
            for (; i < tokens.Count && tokens[i].Value != ")"; i++)
            {
                parameters.Add(ParseExpression());
                if (tokens[i].Value == ")") 
                    break;
                if (tokens[i].Value != ",")
                {
                    AddError(", expected");
                    i--;
                }
            }
            if(i == tokens.Count)
                AddError(") expected");
            return parameters;
        }
        IExpression ParseExpression()
        {
            throw new NotImplementedException();
        }
        bool ValidateParameters(List<IExpression> parameters, string sequence)
        {
            if(parameters.Count != admissibleParameters[sequence].Count)
                return false;
            for(int j = 0;j < parameters.Count; j++)
            {
                if (parameters[j] == null || (parameters[j].GetType() != admissibleParameters[sequence][j]))
                {
                    return false;
                }
            }
            return true;
        }





        CodeLocation BuildLocation((int,int) location)
        {
            return new CodeLocation(location.Item1, location.Item2, tokens[i].Location.Item2);
        }
        void AddError(string text)
        {
            errors.Add(new Error(text, tokens[i].Location));
        }

        void RegisterParameters()
        {
            admissibleParameters[""] = new List<Type>();
            admissibleParameters["int"] = new List<Type> { typeof(int)};
            admissibleParameters["int,int"] = new List<Type> {typeof(int), typeof(int) };
            admissibleParameters["int,int,int"] = new List<Type> { typeof(int), typeof(int), typeof(int) };
            admissibleParameters["int,int,int,int"] = new List<Type> { typeof(int), typeof(int), typeof(int), typeof(int) };
            admissibleParameters["int,int,int,int,int"] = new List<Type> { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) };
            admissibleParameters["string"] = new List<Type> { typeof(string)};
            admissibleParameters["string,int,int"] = new List<Type> { typeof(string), typeof(int), typeof(int) };
            admissibleParameters["string,int,int,int,int"] = new List<Type> { typeof(string), typeof(int), typeof(int), typeof(int), typeof(int) };

        }
    }
}
