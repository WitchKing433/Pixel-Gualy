/* Here we parse the token's list and return the corresponding AST nodes.
The Stream is useful to iterate over the token's list */
using GualyCore;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace GualyLexer
{
    public class Parser
    {
        Dictionary<string, ExpressionValue> variables;
        List<CompilingError> errors;

        public Parser(TokenStream tokenStream, Dictionary<string, ExpressionValue> variables, List<CompilingError> errors)
        {
            Stream = tokenStream;
            this.variables = variables;
            this.errors = errors;
        }
        public TokenStream Stream { get; private set; }

        public List<IExpression> ParseParameters()
        {
            List<IExpression> expressions = new();

            if (!Stream.Next(TokensNames.OpenBracket))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "( expected"));
            }
            if (!Stream.Next(TokensNames.ClosedBracket))
            {
                do
                {
                    expressions.Add(ParseExpression());
                }
                while (Stream.Next(TokensNames.ParametersSeparator));

                if (!Stream.Next(TokensNames.ClosedBracket))
                {
                    errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, ") expected"));
                }
            }
            return expressions;
        }


        public Executer ParseProgram(List<CompilingError> errors)
        {
            Executer executer = new Executer();

            if (!Stream.CanLookAhead(0))
                return executer;

            string tokenName;
            tokenName = Stream.LookAhead().Name;
            if (tokenName != TokensNames.Spawn)
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "Spawn expected"));
            }
            else
            {
                Spawn spawn = ParseSpawn(errors);
            }
            /* Here we parse all the declared elements */
            while (tokenName == Stream.LookAhead().Name)
            {
               
                Element element = ParseElement(errors);
                executer.Elements[element.Id] = element;

                /* After every Element declaration must be a ; */
                if (!Stream.Next(TokensNames.StatementSeparator))
                {
                    errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
                    return executer;
                }

                if (!Stream.Next())
                {
                    break;
                }
            }
            return executer;
        }

        public Spawn ParseSpawn(List<CompilingError> errors)
        {

            Spawn spawn = new Spawn(Stream.LookAhead().Location);
            List<IExpression> expressions = ParseParameters(errors);

            
            if (!Stream.Next(TokensNames.OpenCurlyBraces))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "{ expected"));
            }

            if (!Stream.Next(TokensNames.weak))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "weak expected"));
            }
            if (!Stream.Next(TokensNames.Assign))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "= expected"));
            }
            while (Stream.Next(TokenType.Identifier))
            {
                element.Weak.Add(Stream.LookAhead().Value);
            }
            if (!Stream.Next(TokensNames.StatementSeparator))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
            }

            if (!Stream.Next(TokensNames.strong))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "strong expected"));
            }
            if (!Stream.Next(TokensNames.Assign))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "= expected"));
            }

            while (Stream.Next(TokenType.Identifier))
            {
                element.Strong.Add(Stream.LookAhead().Value);
            }
            if (!Stream.Next(TokensNames.StatementSeparator))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
            }
            if (!Stream.Next(TokensNames.ClosedCurlyBraces))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "} expected"));
            }
            return element;
        }

        /* C -> Card id { power = Exp ; elements = [id] ; } */
        public Card ParseCard(List<CompilingError> errors)
        {

            Card card = new Card("null", Stream.LookAhead().Location);

            if (!Stream.Next(TokenType.Identifier))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "id expected"));
            }
            else
            {
                card.Id = Stream.LookAhead().Value;
            }
            if (!Stream.Next(TokensNames.OpenCurlyBraces))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "{ expected"));
            }

            if (!Stream.Next(TokensNames.power))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "power expected"));
            }
            if (!Stream.Next(TokensNames.Assign))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "= expected"));
            }

            /* Here we parse the expression. If null is returned, we send an error */
            Expression? exp = ParseExpression();
            if (exp == null)
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Invalid, "Bad expression"));
                return card;
            }
            card.Power = exp;

            if (!Stream.Next(TokensNames.StatementSeparator))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
            }
            if (!Stream.Next(TokensNames.elements))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "elements expected"));
            }
            if (!Stream.Next(TokensNames.Assign))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "= expected"));
            }
            while (Stream.Next(TokenType.Identifier))
            {
                card.cardElements.Add(Stream.LookAhead().Value);
            }
            if (!Stream.Next(TokensNames.StatementSeparator))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "; expected"));
            }
            if (!Stream.Next(TokensNames.ClosedCurlyBraces))
            {
                errors.Add(new CompilingError(Stream.LookAhead().Location, ErrorCode.Expected, "} expected"));
            }

            return card;
        }

        /* Turbidity. You should have been in the conference to understand this part. 
        If you still lost, contact to @Rodrigo43 via Telegram and i will try to explain it again */
        private IExpression? ParseExpression()
        {
            return ParseExpressionLv1(null);
        }

        private IExpression? ParseExpressionLv1(IExpression? left)
        {
            IExpression? newLeft = ParseExpressionLv2(left);
            IExpression? exp = ParseExpressionLv1_(newLeft);
            return exp;
        }

        private IExpression? ParseExpressionLv1_(IExpression? left)
        {
            IExpression? exp = ParseAdd(left);
            if (exp != null)
            {
                return exp;
            }
            exp = ParseSub(left);
            if (exp != null)
            {
                return exp;
            }
            return left;
        }

        private IExpression? ParseExpressionLv2(IExpression? left)
        {
            IExpression? newLeft = ParseExpressionLv3(left);
            return ParseExpressionLv2_(newLeft);
        }

        private IExpression? ParseExpressionLv2_(IExpression? left)
        {
            IExpression? exp = ParseMul(left);
            if (exp != null)
            {
                return exp;
            }
            exp = ParseDiv(left);
            if (exp != null)
            {
                return exp;
            }
            return left;
        }

        private IExpression? ParseExpressionLv3(IExpression? left)
        {
            IExpression? exp = ParseValue();
            if (exp != null)
            {
                return exp;
            }
            exp = ParseText();
            if (exp != null)
            {
                return exp;
            }
            return null;
        }


        private IExpression? ParseAdd(IExpression left)
        {
            Add sum = new Add(Stream.LookAhead().Location);

            if (left == null || !Stream.Next(TokensNames.Add))
                return null;
            if(left is ArithmeticExpression)
                sum.LeftOperand = left as ArithmeticExpression;

            Expression? right = ParseExpressionLv2(null);
            if (right == null)
            {
                Stream.MoveBack(2);
                return null;
            }
            sum.Right = right;

            return ParseExpressionLv1_(sum);
        }

        private Expression? ParseSub(IExpression? left)
        {
            Sub sub = new Sub(Stream.LookAhead().Location);

            if (left == null || !Stream.Next(TokensNames.Sub))
                return null;

            sub.Left = left;

            Expression? right = ParseExpressionLv2(null);
            if (right == null)
            {
                Stream.MoveBack(2);
                return null;
            }
            sub.Right = right;

            return ParseExpressionLv1_(sub);
        }

        private IExpression? ParseMul(IExpression? left)
        {
            Mul mul = new Mul(Stream.LookAhead().Location);

            if (left == null || !Stream.Next(TokensNames.Mul))
                return null;

            mul.Left = left;

            IExpression? right = ParseExpressionLv3(null);
            if (right == null)
            {
                Stream.MoveBack(2);
                return null;
            }
            mul.Right = right;

            return ParseExpressionLv2_(mul);
        }

        private IExpression? ParseDiv(IExpression? left)
        {
            Div div = new Div(Stream.LookAhead().Location);

            if (left == null || !Stream.Next(TokensNames.Div))
                return null;

            div.Left = left;

            IExpression? right = ParseExpressionLv3(null);
            if (right == null)
            {
                Stream.MoveBack(2);
                return null;
            }
            div.Right = right;

            return ParseExpressionLv2_(div);
        }

        private IExpression? ParseNumber()
        {
            if (!Stream.Next(TokenType.Number))
                return null;
            return new Number(int.Parse(Stream.LookAhead().Name), Stream.LookAhead().Location);
        }
        private IExpression? ParseValue()
        {
            IExpression expression = null;
            if (Stream.Next(TokenType.Number))
                expression = new GualyCore.Number(int.Parse(Stream.LookAhead().Name), Stream.LookAhead().Location);
            else if (Stream.Next(TokenType.Text))
                expression = new Text(Stream.LookAhead().Name, Stream.LookAhead().Location);
            else if (Stream.Next(TokenType.Identifier))
                expression = new Variable(Stream.LookAhead().Name, variables, Stream.LookAhead().Location);
            else if (Stream.Next(TokenType.Function))
                expression = new Variable(Stream.LookAhead().Name, variables, Stream.LookAhead().Location);

            return expression;
        }



        private IExpression? ParseText()
        {
            if (!Stream.Next(TokenType.Text))
                return null;
            return new Text(Stream.LookAhead().Name, Stream.LookAhead().Location);
        }
    }
}

