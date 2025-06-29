﻿using Lexer;
using GualyCore;
namespace Parser
{
    public class Parser
    {
        Dictionary<string, List<Type>> admissibleParameters = new Dictionary<string,List<Type>>();
        Dictionary<string,string> instructionParameters = new Dictionary<string,string>();
        Dictionary<string, string> functionParameters = new Dictionary<string, string>();
        Dictionary<string,Type> functionsType = new Dictionary<string, Type>();
        Dictionary<string, int> operatorPrecedence = new Dictionary<string, int>();
        List<Token> tokens;
        public List<Error> errors;
        public Dictionary<string, int> labels;
        List<ILineNode> program;
        delegate bool Predicate();
        public List<Error> Errors {  get { return errors; } private set { } }
        int i = 0;
        public Parser(List<Token> tokens)
        {
            labels = new Dictionary<string, int>();
            this.tokens = tokens;
            errors = new List<Error>();
            program = new List<ILineNode>();
            RegisterParameters();
        }

        public List<ILineNode> Parse()
        {
            i = 0;
            if (i < tokens.Count)
            {
                ParseSpawn();
                for (; IsInRange() && tokens[i].Value != "\n"; i++) ;
                i++;
            }
            else
                AddError("Blank Code");
            for (; IsInRange(); i++)
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
                else
                {
                    AddError("Invalid Token");
                }
                for (; IsInRange() && tokens[i].Value != "\n"; i++) ;
            }
            if (errors.Count > 0)
                return null;
            else
                return program;
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
            if (instruction.Value == "GoTo")
            {
                return ParseGoTo();
            }
            else
            {
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
        }
        bool ParseText()
        {
            Token text = tokens[i];
            if (EndOfLine())
            {
                bool a = true;
                program.Add(new Label(text.Value, BuildLocation(text.Location)));
                if(!labels.ContainsKey(text.Value))
                    labels[text.Value] = text.Location.Item1 - 1;
                else
                    a = false;
                i++;
                return a;
            }
            i++;
            if (IsInRange() && tokens[i].Value == "<-")
            {
                i++;
                if (IsInRange())
                {
                    Token token = tokens[i];
                    Expression expression;
                    if (token.Type == TokenType.Function)
                    {
                        expression = ParseFunction();
                    }
                    else
                    {
                        expression = ParseAssignmentExpression();              //revisar despues donde deja el puntero parse expression 
                    }
                    if (expression == null)
                    {
                        AddError("Invalid expression");
                        return false;
                    }
                    if (IsInRange() && tokens[i].Value != "\n")
                    {
                        AddError("EndOfLine expected");
                        return false;
                    }
                    program.Add(new AssignmentExpression(text.Value, expression, BuildLocation(text.Location)));
                    return true;
                }
                else
                {
                    AddError("Missing expression");
                    return false;
                }
            }
            AddError("EndOfLine or <- expected");
            return false;
        }

        bool ParseGoTo()
        {
            bool unParseable = false;
            Token goTo = tokens[i];
            string label = "";
            i++;
            if (IsInRange())
            {
                if (!ParseByValue("["))
                {
                    unParseable = true;
                }
                if (IsInRange())
                {
                    if (tokens[i].Type != TokenType.Text)
                    {
                        AddError("Label expected");
                        unParseable = true;
                    }
                    else 
                    { 
                        label = tokens[i].Value;
                        i++;
                    }
                    if (IsInRange())
                    {
                        if (!ParseByValue("]"))
                        {
                            unParseable = true;
                        }
                        if (IsInRange())
                        {
                            List<Expression> parameters = ParseParameters();
                            if (!unParseable && ValidateParameters(parameters, "int"))
                            {
                                program.Add(new GoTo(label, BuildLocation(goTo.Location), parameters[0]));
                                i++;
                                return true;
                            }
                        }
                    }
                }
               
            }
            AddError("Missing parameters");
            return false;
        }

        bool ParseByValue(string value)
        {
            if (tokens[i].Value != value)
            {
                AddError($"{value} expected");
                return false;
            }
            i++;
            return true;
        }
        Function ParseFunction()
        {
            Token function = tokens[i];
            i++;
            if (IsInRange())
            {
                List<Expression> parameters = ParseParameters();
                if(ValidateParameters(parameters, functionParameters[function.Value]))
                {
                    if (EndOfLine())
                    {
                        Function func = Function.BuildFunction(BuildLocation(function.Location), functionsType[function.Value], parameters, function.Value);
                        i++;
                        return func;
                    }
                    else
                    {
                        AddError("EndOfLine expected");
                        return null;
                    }
                }
                else
                {
                    AddError("Invalid parameters");
                    return null;
                }
            }
            AddError("Missing parameters");
            return null;
        }
        
        List<Expression> ParseParameters()
        {
            Expression expression;
            List<Expression> parameters = new List<Expression>();
            if (!IsInRange() || tokens[i].Value != "(")
            {
                AddError("( expected");
            }
            else
                i++;
            for (; IsInRange() && tokens[i].Value != ")"; i++)
            {
                expression = ParseParameterExpression(i);
                if(expression == null)
                {
                    AddError("Wrong parameters");
                    return null;
                }
                parameters.Add(expression);
                for (; IsInRange() && tokens[i].Value != "," && !FinalParenthesis() && tokens[i].Value != "\n"; i++) ;
                if(!IsInRange() || tokens[i].Value == "\n" || tokens[i].Value == ")")
                    break;
            }
            if(!IsInRange() || tokens[i].Value == "\n")
                AddError(") expected");
            return parameters;
        }
        bool FinalParenthesis()
        {
            return tokens[i].Value == ")" && IsInRange(i + 1) && tokens[i + 1].Value == "\n";
        }
        ArithmeticExpression ParseArithmeticExpression(int left, int right)
        {
            ArithmeticExpression leftExpression;
            ArithmeticExpression rightExpression;
            i = left;
            int newRight = right;
            string oper = "";
            int operIndex = 0;
            int openPar = 0;
            if (!IsInRange(left) || !IsInRange(right))
            {
                AddError("Invalid Expression");
                return null;
            }
            if (tokens[left].Value == "(" && tokens[right].Value == ")")
            {
                return ParseArithmeticExpression(left + 1, right - 1);
            }
            if (IsInRange())
            {
                for(; IsInRange() && i <= right; i++)
                {
                    if (tokens[i].Value == ")")
                        openPar--;
                    else if (tokens[i].Value == "(")
                        openPar++;
                    if (openPar < 0)    
                    {
                        AddError("Invalid Expression");
                        return null;
                    }
                    if (openPar == 0)
                    {
                        if (tokens[i].Type == TokenType.Operator && operatorPrecedence.ContainsKey(tokens[i].Value))
                        {
                            if (operatorPrecedence[tokens[i].Value] <= operatorPrecedence[oper])
                            {
                                if (!(ProgramData.rightAssociative.Contains(oper) && tokens[i].Value == oper))
                                {
                                    oper = tokens[i].Value;
                                    operIndex = i;
                                }
                            }
                        }
                        if (EndOfLine())
                        {
                            newRight = i;
                            break;
                        }
                    }
                    if (EndOfLine())
                    {
                        if(openPar > 0)
                        {
                            AddError("Invalid Expression");
                            return null;
                        }
                        newRight = i;
                        break;
                    }
                }
                if(oper == "")
                {
                    if (left == right)
                    {
                        if (tokens[left].Type == TokenType.Number)
                        {
                            return new Number(BuildLocation(tokens[left].Location), typeof(int), int.Parse(tokens[left].Value));
                        }
                        else if(tokens[left].Type == TokenType.Text)
                        {
                            return new Variable(tokens[left].Value, BuildLocation(tokens[left].Location), typeof(int));
                        }
                    }
                    AddError("Invalid Expression");
                    return null;
                }
                rightExpression = ParseArithmeticExpression(operIndex + 1, newRight);
                if (rightExpression == null)
                {
                    return null;
                }
                if (!ProgramData.unaryOperators.ContainsKey(oper))
                {
                    leftExpression = ParseArithmeticExpression(left, operIndex - 1);
                    if (leftExpression == null)
                    {
                        return null;
                    }
                    return BinaryOperator.BuildOperator(BuildLocation(tokens[operIndex].Location), leftExpression, rightExpression, oper);
                }
                return UnaryOperator.BuildOperator(oper, BuildLocation(tokens[operIndex].Location),rightExpression);
            }
            else
            {
                AddError("Invalid Expression");
                return null;
            }
        }

        Expression ParseAssignmentExpression()
        {
            int left = i;
            int j = i;
            for (; IsInRange(j) && tokens[j].Value != "\n"; j++) ;
            if(IsInRange(j) && tokens[j].Value == "\n")
            {
                Expression expression = ParseArithmeticExpression(left, j - 1);
                i = j;
                if(expression != null)
                    return expression;
            }
            return null;
        }
        Expression ParseExpression(int left, int right)
        {
            Expression expression = ParseString();
            if (expression == null)
            {
                i = left;
                expression = ParseArithmeticExpression(left, right);
                if (expression == null)
                {
                    AddError("Invalid expression");
                    return null;
                }
            }
            return expression;
        }
        Text ParseString()
        {
            if (IsInRange() && tokens[i].Value == "\"")
            {
                Token token = tokens[i];
                i++;
                if(IsInRange() && tokens[i].Type == TokenType.Text)
                {
                    Token text = tokens[i];
                    i++;
                    {
                        if(IsInRange() && tokens[i].Value == "\"")
                        {
                            return new Text(BuildLocation(token.Location), typeof(string), text.Value);
                        }
                    }
                }
            }
            return null;

        }
        Expression ParseParameterExpression(int left)
        {
            int j = left;
            for (; IsInRange(j) && tokens[j].Value != "," && tokens[j].Value != "\n"; j++);
            if (IsInRange(j))
            {
                if (tokens[j].Value == ",")
                {
                    return ParseExpression(left, j - 1);
                }
                if (tokens[j].Value == "\n")
                {
                    if (tokens[j - 1].Value == ")")
                        return ParseExpression(left, j - 2);
                }
            }
            return null;
        }
        bool ValidateParameters(List<Expression> parameters, string sequence)
        {
            if(parameters == null)
            {
                return false;
            }
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
            if (IsInRange())
                errors.Add(new Error(text, tokens[i].Location));
            else if (tokens.Count > 0)
                errors.Add(new Error(text, tokens[tokens.Count - 1].Location));
            else
                errors.Add(new Error(text, (0, 0)));
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
        bool IsInRange(int r)
        {
            return r < tokens.Count;
        }
        bool SkipLine()
        {
            bool flag  = SkipToTokenByPredicate("\n", IsInRange);
            if (flag)
                i++;
            return flag;
        }
        bool SkipToToken(string text)
        {
            return SkipToTokenByPredicate(text, SameLine);
        }
        bool SkipToTokenByPredicate(string text, Predicate predicate)
        {
            for (; predicate(); i++)
            {
                if (tokens[i].Value == text)
                {
                    break;
                }
            }
            return (predicate());
        }
        bool SameLine()
        {
            return IsInRange() && !(tokens[i].Value == "\n");
        }
        void RegisterParameters()
        {
            Type tInt = typeof(int);
            Type tString = typeof(string);
            //Parameters
            admissibleParameters[""] = new List<Type>();
            admissibleParameters["int"] = new List<Type> { tInt};
            admissibleParameters["int,int"] = new List<Type> {tInt, tInt };
            admissibleParameters["int,int,int"] = new List<Type> { tInt, tInt, tInt };
            admissibleParameters["int,int,int,int"] = new List<Type> { tInt, tInt, tInt, tInt };
            admissibleParameters["int,int,int,int,int"] = new List<Type> { tInt, tInt, tInt, tInt, tInt };
            admissibleParameters["string"] = new List<Type> { tString};
            admissibleParameters["string,int,int"] = new List<Type> { tString, tInt, tInt };
            admissibleParameters["string,int,int,int,int"] = new List<Type> { tString, tInt, tInt, tInt, tInt };

            //Instructions
            instructionParameters["Color"] = "string";
            instructionParameters["Size"] = "int";
            instructionParameters["DrawLine"] = "int,int,int";
            instructionParameters["DrawCircle"] = "int,int,int";
            instructionParameters["DrawRectangle"] = "int,int,int,int,int";
            instructionParameters["Fill"] = "";

            //Functions
            functionParameters["GetActualX"] = "";
            functionParameters["GetActualY"] = "";
            functionParameters["GetCanvasSize"] = "";
            functionParameters["GetColorCount"] = "string,int,int,int,int";
            functionParameters["IsBrushColor"] = "string";
            functionParameters["IsBrushSize"] = "int";
            functionParameters["IsCanvasColor"] = "string,int,int";

            //FunctionsType
            functionsType["GetActualX"] = tInt;
            functionsType["GetActualY"] = tInt;
            functionsType["GetCanvasSize"] = tInt;
            functionsType["GetColorCount"] = tInt;
            functionsType["IsBrushColor"] = tInt;
            functionsType["IsBrushSize"] = tInt;
            functionsType["IsCanvasColor"] = tInt;

            //OperatorPrecedence
            operatorPrecedence[""] = int.MaxValue;
            for (int k = 0; k < ProgramData.listOperatorPrecedence.Count; k++)
            {
                for(int l = 0; l < ProgramData.listOperatorPrecedence[k].Count; l++)
                {
                    operatorPrecedence[ProgramData.listOperatorPrecedence[k][l]] = k;
                }
            }
        }
    }
}
