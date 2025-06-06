using Lexer;
using GualyCore;
namespace Parser
{
    public class Parser
    {
        Dictionary<string, List<Type>> admissibleParameters = new Dictionary<string,List<Type>>();
        Dictionary<string,string> instructionParameters = new Dictionary<string,string>();
        List<Token> tokens;
        List<Error> errors;
        public Dictionary<string, int> labels;
        public Dictionary<string, Expression> variables;
        List<ILineNode> program;
        public List<Error> Errors {  get { return errors; } private set { } }
        int i = 0;
        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            errors = new List<Error>();
            program = new List<ILineNode>();
            RegisterParameters();
        }

        public List<ILineNode> Parse()
        {
            program = new List<ILineNode>();
            i = 0;
            if (i < tokens.Count)
                ParseSpawn();
            else
                AddError("Blank Code");
            for (; i < tokens.Count; i++)
            {
                if (tokens[i].Type == TokenType.Instruction)
                {
                    ParseInstruction();
                    continue;
                }
                else if(tokens[i].Type == TokenType.Text)
                {
                    ParseText();
                    continue;
                }
            }
        }

        bool ParseSpawn()
        {
            if (IsInRange() && tokens[0].Value == "Spawn")
            {
                (int, int) location = tokens[0].Location;
                i++;
                List<Expression> parameters = ParseParameters();
                if (ValidateParameters(parameters, "int,int"))
                {
                    if (EndOfLine())
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
        bool ParseInstruction()
        {
            Token instruction = tokens[i];
            if (instruction.Value == "Spawn")
            {
                AddError("Spawn can only be used on the first line and as the first instruction");
                return false;
            }
            i++;
            List<Expression> parameters = ParseParameters();
            if (ValidateParameters(parameters, instructionParameters[instruction.Value]))
            {
                if (EndOfLine())
                    return AddInstruction(parameters, instruction);
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
        bool ParseText()
        {
            Token text = tokens[i];
            if (EndOfLine())
            {
                program.Add(new Label(text.Value, BuildLocation(text.Location)));
                i++;
                return true;
            }
            i++;
            if (tokens[i].Value == "<-")
            {
                i++;
                Expression expression = ParseExpression();              //revisar despues donde deja el puntero parse expression 
                if(expression == null)
                {
                    AddError("Invalid expression");
                    return false;
                }
                if (!EndOfLine())
                {
                    AddError("EndOfLine expected");
                    return false;
                }
                variables[text.Value] = expression;
                program.Add(new AssignmentExpression(text.Value, expression, BuildLocation(text.Location)));
                return true;
            }
            AddError("EndOfLine or <- expected");
            return false;
        }
        Variable ParseVariable()
        {
            if (variables.ContainsKey(tokens[i].Value))
            {
                return new Variable(tokens[i].Value, BuildLocation(tokens[i].Location), variables[tokens[i].Value].ExprType);
            }
            AddError("Missing variable");
            return null;
        }

        List<Expression> ParseParameters()
        {
            List<Expression> parameters = new List<Expression>();
            if (!IsInRange() || tokens[i].Value != "(")
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
            if(!IsInRange())
                AddError(") expected");
            return parameters;
        }
        Expression ParseExpression()
        {
            throw new NotImplementedException();
        }
        bool ValidateParameters(List<Expression> parameters, string sequence)
        {
            if(parameters.Count != admissibleParameters[sequence].Count)
                return false;
            for(int j = 0;j < parameters.Count; j++)
            {
                if (parameters[j] == null || (parameters[j].ExprType != admissibleParameters[sequence][j]))
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
        bool AddInstruction(List<Expression> parameters, Token instruction)
        {
            program.Add(Instruction.BuildInstruction(BuildLocation(instruction.Location), parameters, instruction.Value));
            i++;
            return true;
        }
        bool EndOfLine()
        {
            return i + 1 < tokens.Count && tokens[i + 1].Value == "\n";
        }
        bool IsInRange()
        {
            return i < tokens.Count;
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
        void RegisterInstructionParameters()
        {
            instructionParameters["Color"] = "string";
            instructionParameters["Size"] = "int";
            instructionParameters["DrawLine"] = "int,int,int";
            instructionParameters["DrawCircle"] = "int,int,int";
            instructionParameters["DrawRectangle"] = "int,int,int,int,int";
            instructionParameters["Fill"] = "";
        }
    }
}
