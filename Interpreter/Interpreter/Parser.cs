using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Xml.Schema;
using Interpreter.Exceptions;
using Interpreter.Ast;
using Interpreter.Interfaces;
using Interpreter.Values;
using Expression = Interpreter.Ast.Expression;
using String = Interpreter.Values.String;

namespace Interpreter
{
    public class Parser
    {
        private readonly Lexer lexer;
        private Token token;
        private readonly List<IError> errors = new List<IError>();
        private readonly List<Goto> gotos = new List<Goto>();
        private const string print = "Print";
        private const string operatorNew = "new";

        private readonly Dictionary<Label, Statement> labelToStatement =
            new Dictionary<Label, Statement>();

        private readonly string[] keyWords =
        {
            "int", "double", "bool", "string", "for", "while", "goto", "if",
            "Print", "new"
        };

        #region Enums
        public enum MethodType
        {
            Sin,
            Cos
        }

        public enum BinaryOperationType
        {
            Plus,
            Minus,
            Divide,
            Multiply,
            Power
        }

        public enum UnaryOperationType
        {
            Plus,
            Minus,
        }

        public enum CompareOperatorType
        {
            Less,
            LessOrEqual,
            Equal,
            NotEqual,
            GreaterOrEqual,
            Greater
        }
        #endregion

        #region DictionariesInitialization
        private readonly Dictionary<string, MethodType> methodOperationToMethodOperationType =
        new Dictionary<string, MethodType>
                    {
                        {"sin", MethodType.Sin},
                        {"cos", MethodType.Cos}
                    };

        private readonly Dictionary<TokenType, BinaryOperationType> binaryOperationToBinaryOperationType =
            new Dictionary<TokenType, BinaryOperationType>()
                    {
                        {TokenType.Plus, BinaryOperationType.Plus},
                        {TokenType.Minus, BinaryOperationType.Minus},
                        {TokenType.Divide, BinaryOperationType.Divide},
                        {TokenType.Multiply, BinaryOperationType.Multiply},
                        {TokenType.Degree, BinaryOperationType.Power},
                    };

        private readonly Dictionary<TokenType, UnaryOperationType> unaryOperationToUnaryOperationType =
            new Dictionary<TokenType, UnaryOperationType>()
                    {
                        {TokenType.Plus, UnaryOperationType.Plus},
                        {TokenType.Minus, UnaryOperationType.Minus},
                    };

        private readonly Dictionary<TokenType, CompareOperatorType> boolOperatorToBoolOperatorType =
            new Dictionary<TokenType, CompareOperatorType>()
                    {
                        {TokenType.Less, CompareOperatorType.Less},
                        {TokenType.LessOrEqual, CompareOperatorType.LessOrEqual},
                        {TokenType.Equal, CompareOperatorType.Equal},
                        {TokenType.NotEqual, CompareOperatorType.NotEqual},
                        {TokenType.GreaterOrEqual, CompareOperatorType.GreaterOrEqual},
                        {TokenType.Greater, CompareOperatorType.Greater},
                    };

        private readonly Dictionary<string, ValueType> typeToValueType =
                new Dictionary<string, ValueType>()
                    {
                        {"string", ValueType.String},
                        {"double", ValueType.Double},
                        {"int", ValueType.Int},
                        //{"bool", ValueType.Bool},
                    };
        #endregion

        public Parser(string programText)
        {
            if (programText == null)
            {
                throw new ArgumentNullException();
            }

            lexer = new Lexer(programText, errors);
        }

        public Ast.Program Program()
        {
            //try
            //{
                Ast.Program result = ParseProgram();
                CombineGotoWithLabels();
                return result;
            //}
            //catch (LexerException ex)
            //{
            //    errors.Add(new Error(ex));
            //    return null;
            //}
        }

        private void CombineGotoWithLabels()
        {
            foreach (var @goto in gotos)
            {
                if (labelToStatement.ContainsKey(@goto.Label))
                {
                    @goto.NextStatement = labelToStatement[@goto.Label];
                    continue;
                }

                errors.Add(new Error(new ParserException(ParserException.ExceptionType.LableNotFound,
                    @goto.Label.Position)));
            }
        }

        private Ast.Program ParseProgram()
        {
            List<Statement> statements = new List<Statement>();
            Position startPosition = new Position();

            while (true)
            {
                try
                {
                    Statement statement = ParseStatement();
                    if (statement == null)
                    {
                        break;
                    }

                    statements.Add(statement);
                }
                catch (ParserException parseException)
                {
                    errors.Add(new Error(parseException));
                    SkipTokens();

                    token = lexer.Peek();

                    if (token.Type != TokenType.Semicolon)
                    {
                        lexer.TokenBack();
                    }
                }
                catch (LexerException lexerException)
                {
                    errors.Add(new Error(lexerException));
                    SkipTokens();

                    token = lexer.Peek();

                    if (token.Type != TokenType.Semicolon)
                    {
                        lexer.TokenBack();
                    }
                }
            }
            token = lexer.Peek();

            if (token.Type != TokenType.Eof)
            {
                Position startError = token.Position;
                lexer.SkipTokensUntilToken(TokenType.Eof);
                Position endError = lexer.Peek().Position;
                errors.Add(new Error(new ParserException(ParserException.ExceptionType.UnknownCode,
                    new Position(startError, endError))));
                return null;
            }

            Position endPosition = token.Position;
            Block block = new Block(statements, new Position(startPosition, endPosition));
            return new Ast.Program(block.GetFirstStatement(), new Position(startPosition, endPosition));
        }

        private Statement ParseStatement()
        {
            token = lexer.GetNextTokenForParser();
            lexer.TokenBack();

            //Default isn't needed
            switch (token.Type)
            {
                case TokenType.Eof:
                case TokenType.CloseCurlyBrace:
                    lexer.GetNextTokenForParser();
                    return null;
                case TokenType.OpenCurlyBrace:
                    return ParseBlock();
                case TokenType.If:
                    return ParseIf();
                case TokenType.Else:
                    return ParseElse();
                case TokenType.While:
                    return ParseWhile();
                case TokenType.For:
                    return ParseFor();
                case TokenType.Goto:
                    return ParseGoto();
                case TokenType.Identifier:
                    Position errorPosition;
                    Statement result;

                    if ((result = ParsePrint()) != null)
                    {
                        return result;
                    }

                    if ((result = ParseDeclaration()) != null)
                    {
                        if (token.Type == TokenType.Semicolon)
                        {
                            return result;
                        }

                        errorPosition = lexer.GetPrevToken().GetNextPosition();
                        throw new ParserException(ParserException.ExceptionType.MissingSemicolon, errorPosition);
                    }

                    if ((result = ParseLabel()) != null)
                    {
                        return result;
                    }

                    if ((result = ParseAssignment()) != null)
                    {
                        if (token.Type == TokenType.Semicolon)
                        {
                            return result;
                        }

                        errorPosition = lexer.GetPrevToken().GetNextPosition();
                        throw new ParserException(ParserException.ExceptionType.MissingSemicolon, errorPosition);
                    }

                    errorPosition = lexer.GetPrevToken().Position;
                    SkipTokens();
                    throw new ParserException(ParserException.ExceptionType.IncorrectStatement, errorPosition);
                default:
                    lexer.GetNextTokenForParser();
                    errorPosition = lexer.Peek().Position;
                    SkipTokens();
                    errors.Add(new Error(new ParserException(ParserException.ExceptionType.IncorrectStatement, errorPosition)));
                    return null;
            }
        }

        private Statement ParseLabel()
        {
            lexer.GetNextTokenForParser();
            token = lexer.GetNextTokenForParser();

            if (token.Type != TokenType.Colon)
            {
                lexer.TokenBack();
                lexer.TokenBack();
                return null;
            }

            Label label = new Label(lexer.GetPrevToken().Name, lexer.GetPrevToken().Position);
            Statement statement = ParseStatement();

            if (labelToStatement.ContainsKey(label))
            {
                throw new ParserException(ParserException.ExceptionType.LableDuplicate, token.Position);
            }

            labelToStatement.Add(label, statement);
            return statement;
        }

        private Printer ParsePrint()
        {
            token = lexer.GetNextTokenForParser();
            Position startPosition = token.Position;

            if (token.Name != print)
            {
                lexer.TokenBack();
                return null;
            }

            token = lexer.GetNextTokenForParser();
            if (token.Type != TokenType.OpenParenthesis)
            {
                throw new ParserException(ParserException.ExceptionType.MissingOpenParenthesis,
                    token.GetNextPosition());
            }

            Expression expression = ParseExpression();

            if (token.Type != TokenType.CloseParenthesis)
            {
                throw new ParserException(ParserException.ExceptionType.MissingCloseParenthesis,
                    lexer.Peek().GetNextPosition());
            }

            token = lexer.GetNextTokenForParser();
            if (token.Type != TokenType.Semicolon)
            {
                throw new ParserException(ParserException.ExceptionType.MissingSemicolon,
                    lexer.Peek().GetNextPosition());
            }

            return new Printer(expression, new Position(startPosition, token.Position));
        }

        private Goto ParseGoto()
        {
            token = lexer.GetNextTokenForParser();
            Position startPosition = token.Position;

            token = lexer.GetNextTokenForParser();

            if (token.Type != TokenType.Identifier)
            {
                throw new ParserException(ParserException.ExceptionType.MissingVariable, token.Position);
            }

            if (IsKeyWord(token.Name))
            {
                throw new ParserException(ParserException.ExceptionType.IdentifierIsKeyWord, token.Position);
            }

            Token gotoLabelToken = token;

            token = lexer.GetNextTokenForParser();
            if (token.Type != TokenType.Semicolon)
            {
                throw new ParserException(ParserException.ExceptionType.MissingSemicolon, token.Position);
            }

            gotos.Add(new Goto(new Label(gotoLabelToken.Name, gotoLabelToken.Position),
                new Position(startPosition, token.Position)));
            return gotos.Last();
        }

        private Block ParseBlock()
        {
            List<Statement> statements = new List<Statement>();
            token = lexer.GetNextTokenForParser();
            Position startPosition = token.Position;

            if (token.Type != TokenType.OpenCurlyBrace)
            {
                lexer.TokenBack();
                return null;
            }

            while (true)
            {
                try
                {
                    Statement statement = ParseStatement();
                    if (statement == null)
                    {
                        break;
                    }
                    statements.Add(statement);
                }
                catch (ParserException parseException)
                {
                    errors.Add(new Error(parseException));
                    SkipTokens();

                    token = lexer.Peek();

                    if (token.Type == TokenType.CloseCurlyBrace)
                    {
                        break;
                    }

                    if (token.Type != TokenType.Semicolon)
                    {
                        lexer.TokenBack();
                    }
                }

                catch (LexerException lexerException)
                {
                    errors.Add(new Error(lexerException));
                    SkipTokens();

                    token = lexer.Peek();

                    if (token.Type == TokenType.CloseCurlyBrace)
                    {
                        break;
                    }

                    if (token.Type != TokenType.Semicolon)
                    {
                        lexer.TokenBack();
                    }
                }
            }

            token = lexer.Peek();
            Position endPosition = token.Position;
            if (token.Type != TokenType.CloseCurlyBrace)
            {
                errors.Add(new Error(new ParserException(
                    ParserException.ExceptionType.MissingCloseBlockBracket,
                    lexer.GetPrevToken().GetNextPosition())));
            }

            return new Block(statements, new Position(startPosition, endPosition));
        }

        private void SkipTokens()
        {
            lexer.SkipTokensUntilToken(TokenType.Semicolon, TokenType.OpenCurlyBrace,
                        TokenType.If, TokenType.While, TokenType.For);
        }

        private IfStatement ParseIf()
        {
            if ((token = lexer.GetNextTokenForParser()).Type != TokenType.If)
            {
                lexer.TokenBack();
                return null;
            }

            Position startPosition = token.Position;

            if ((token = lexer.GetNextTokenForParser()).Type != TokenType.OpenParenthesis)
            {
                throw new ParserException(ParserException.ExceptionType.MissingOpenParenthesis,
                    lexer.GetPrevToken().GetNextPosition());
            }

            Condition condition = ParseCondition();
            if (condition == null)
            {
                throw new ParserException(ParserException.ExceptionType.MissingCondition,
                     lexer.GetPrevToken().GetNextPosition());
            }

            if (token.Type != TokenType.CloseParenthesis)
            {
                throw new ParserException(ParserException.ExceptionType.MissingCloseParenthesis,
                    lexer.GetPrevToken().GetNextPosition());
            }

            Statement trueStatement = ParseStatement();

            if (trueStatement == null)
            {
                throw new ParserException(ParserException.ExceptionType.IncorrectStatement,
                    token.Position);
            }

            Statement elseStatement = ParseElse();

            IfStatement ifStatement = new IfStatement(condition, trueStatement,
                    new Position(startPosition, lexer.GetPrevToken().Position));
            if (elseStatement == null)
                return ifStatement;

            ifStatement.NextStatement = elseStatement;
            return ifStatement;
        }

        private ElseStatement ParseElse()
        {
            if ((token = lexer.GetNextTokenForParser()).Type != TokenType.Else)
            {
                lexer.TokenBack();
                return null;
            }

            Position startPosition = token.Position;

            Statement nextStatement = ParseStatement();
            if (nextStatement == null)
            {
                throw new ParserException(ParserException.ExceptionType.IncorrectStatement,
                    new Position(startPosition, token.Position));
            }

            return new ElseStatement(nextStatement, new Position(startPosition, token.Position));
        }

        private WhileStatement ParseWhile()
        {
            if ((token = lexer.GetNextTokenForParser()).Type != TokenType.While)
            {
                lexer.TokenBack();
                return null;
            }

            Position startPosition = token.Position;

            if ((token = lexer.GetNextTokenForParser()).Type != TokenType.OpenParenthesis)
            {
                throw new ParserException(ParserException.ExceptionType.MissingOpenParenthesis,
                    lexer.GetPrevToken().GetNextPosition());
            }

            Condition condition = ParseCondition();
            if (condition == null)
            {
                throw new ParserException(ParserException.ExceptionType.MissingCondition,
                     lexer.GetPrevToken().GetNextPosition());
            }

            if (token.Type != TokenType.CloseParenthesis)
            {
                throw new ParserException(ParserException.ExceptionType.MissingCloseParenthesis,
                    lexer.GetPrevToken().GetNextPosition());
            }

            Statement trueStatement = ParseStatement();

            if (trueStatement == null)
            {
                throw new ParserException(ParserException.ExceptionType.IncorrectStatement,
                    new Position(startPosition, token.Position));
            }

            return new WhileStatement(condition, trueStatement, new Position(startPosition, lexer.GetPrevToken().Position));
        }

        private ForStatement ParseFor()
        {
            token = lexer.GetNextTokenForParser();
            Position startPosition = token.Position;

            if ((token = lexer.GetNextTokenForParser()).Type != TokenType.OpenParenthesis)
            {
                throw new ParserException(ParserException.ExceptionType.MissingOpenParenthesis,
                    lexer.GetPrevToken().GetNextPosition());
            }

            Statement initializer = ParseDeclaration() ?? (Statement)ParseAssignment();

            if (initializer == null)
            {
                throw new ParserException(ParserException.ExceptionType.MissingDeclarationOrAssignment,
                    lexer.GetPrevToken().Position);
            }

            if (token.Type != TokenType.Semicolon)
            {
                throw new ParserException(ParserException.ExceptionType.MissingSemicolon, lexer.GetPrevToken().Position);
            }

            Condition condition = ParseCondition();

            if (token.Type != TokenType.Semicolon)
            {
                throw new ParserException(ParserException.ExceptionType.MissingSemicolon, lexer.GetPrevToken().Position);
            }

            Statement iterator = ParseAssignment();

            if (iterator == null)
            {
                SkipTokens();
                throw new ParserException(ParserException.ExceptionType.MissingAssignment,
                    lexer.GetPrevToken().Position);
            }

            if (token.Type != TokenType.CloseParenthesis)
            {
                throw new ParserException(ParserException.ExceptionType.MissingCloseParenthesis,
                    lexer.GetPrevToken().GetNextPosition());
            }

            Statement trueStatement = ParseStatement();

            if (trueStatement == null)
            {
                throw new ParserException(ParserException.ExceptionType.IncorrectStatement,
                    new Position(startPosition, token.Position));
            }

            return new ForStatement(initializer, condition, iterator, trueStatement, new Position(startPosition, token.Position));
        }

        private Condition ParseCondition()
        {
            BoolOperator boolOperator = ParseBoolOperator();
            return new Condition(boolOperator, boolOperator.Position);
        }

        private BoolOperator ParseBoolOperator()
        {
            Expression leftExpression = ParseExpression();

            if (!boolOperatorToBoolOperatorType.ContainsKey(token.Type))
            {
                throw new ParserException(ParserException.ExceptionType.MissingCompareOperator, token.GetNextPosition());
            }

            return new BoolOperator(boolOperatorToBoolOperatorType[token.Type], leftExpression,
                ParseExpression(), new Position(leftExpression.Position, token.Position));
        }

        private Declaration ParseDeclaration()
        {
            token.GetNextPosition();
            Position startPosition = token.Position;

            ValueType internalType;
            ValueType declarationValueType = ParseValueType(out internalType);

            if (declarationValueType == ValueType.Null)
            {
                return null;
            }

            token = lexer.GetNextTokenForParser();

            if (token.Type != TokenType.Identifier)
            {
                throw new ParserException(ParserException.ExceptionType.MissingVariable, token.Position);
            }

            Variable variable = new Variable(token.Name, token.Position);
            token = lexer.GetNextTokenForParser();

            switch (token.Type)
            {
                case TokenType.Semicolon:
                    return new Declaration(declarationValueType, internalType, variable, null, new Position(startPosition, token.Position));
                case TokenType.Assignment:
                    Expression expression = ParseArrayInitilizer() ?? ParseExpression();
                    return new Declaration(declarationValueType, internalType, variable, expression, new Position(startPosition, token.Position));
            }

            throw new ParserException(ParserException.ExceptionType.MissingSemicolon, lexer.GetPrevToken().GetNextPosition());
        }

        private Expression ParseArrayInitilizer()
        {
            token = lexer.GetNextTokenForParser();
            Position startPosition = token.Position;

            if (token.Type != TokenType.Identifier || !token.Name.Equals(operatorNew, StringComparison.CurrentCulture))
            {
                lexer.TokenBack();
                return null;
            }

            token.GetNextPosition();
            ValueType internalType = ParseValueType();
            if (internalType == ValueType.Null)
            {
                throw new ParserException(ParserException.ExceptionType.MissingType, lexer.GetPrevToken().GetNextPosition());
            }

            Expression numberOfElements = ParseIndexer();
            if (numberOfElements == null)
            {
                throw new ParserException(ParserException.ExceptionType.MissingIndexer, token.GetNextPosition());
            }

            return new ArrayInitializer(numberOfElements, internalType, new Position(startPosition, token.Position));
        }

        /// <param name="internalType">internalType - for arrays, default - ValueType.Null for not Array type</param>
        private ValueType ParseValueType(out ValueType internalType)
        {
            internalType = ValueType.Null;
            token = lexer.GetNextTokenForParser();
            if (!typeToValueType.Keys.Contains(token.Name))
            {
                lexer.TokenBack();
                return ValueType.Null;
            }

            string type = token.Name;

            token = lexer.GetNextTokenForParser();

            if (token.Type == TokenType.OpenBracket)
            {
                token = lexer.GetNextTokenForParser();

                if (token.Type != TokenType.CloseBracket)
                {
                    throw new ParserException(ParserException.ExceptionType.MissingCloseBracket,
                        lexer.GetPrevToken().GetNextPosition());
                }

                internalType = typeToValueType[type];
                return ValueType.Array;
            }

            lexer.TokenBack();
            return typeToValueType[type];
        }

        private ValueType ParseValueType()
        {
            token = lexer.GetNextTokenForParser();
            if (!typeToValueType.Keys.Contains(token.Name))
            {
                lexer.TokenBack();
                return ValueType.Null;
            }

            return typeToValueType[token.Name];
        }

        private Assignment ParseAssignment()
        {
            token = lexer.GetNextTokenForParser();
            Position startPosition = token.Position;

            if (token.Type != TokenType.Identifier || methodOperationToMethodOperationType.ContainsKey(token.Name))
            {
                lexer.TokenBack();
                return null;
            }

            Variable variable = new Variable(token.Name, token.Position);
            Expression destination = ParseSlice(variable) ?? variable;

            if (destination is Variable)
            {
                token = lexer.GetNextTokenForParser();

            }

            if (token.Type != TokenType.Assignment)
            {
                throw new ParserException(ParserException.ExceptionType.MissingAssignment,
                    lexer.GetPrevToken().Position);
            }

            Expression expression = ParseArrayInitilizer() ?? ParseExpression();
            if (expression == null)
            {
                throw new ParserException(ParserException.ExceptionType.IncorrectExpression,
                    lexer.GetPrevToken().Position);
            }

            return new Assignment(destination, expression, new Position(startPosition, token.Position));
        }

        private Expression ParseExpression()
        {
            Expression expression = ParseTerm();

            while (token.Type == TokenType.Plus || token.Type == TokenType.Minus)
            {
                Token curOperationToken = token;
                expression = new BinaryOperation(binaryOperationToBinaryOperationType[curOperationToken.Type], expression,
                    ParseTerm(), new Position(expression.Position, token.Position));
            }

            return expression;
        }

        private Expression ParseTerm()
        {
            Expression expression = ParseDegree();

            while (token.Type == TokenType.Multiply || token.Type == TokenType.Divide)
            {
                Token curOperationToken = token;
                expression = new BinaryOperation(binaryOperationToBinaryOperationType[curOperationToken.Type], expression,
                    ParseDegree(), new Position(expression.Position, token.Position));
            }

            return expression;
        }

        private Expression ParseDegree()
        {
            Expression leftExpression = ParseUnaryOperation();

            if (leftExpression is Slice)
            {
                lexer.TokenBack();
            }

            token = lexer.GetNextTokenForParser();
            if (token.Type == TokenType.Degree)
            {
                Token curOperationToken = token;
                return new BinaryOperation(binaryOperationToBinaryOperationType[curOperationToken.Type], leftExpression,
                    ParseDegree(), new Position(leftExpression.Position, token.Position));
            }

            return leftExpression;
        }

        private Expression ParseUnaryOperation()
        {
            Expression expression;
            token = lexer.GetNextTokenForParser();
            UnaryOperationType unOpType;
            Position startPosition = token.Position;

            if (!unaryOperationToUnaryOperationType.ContainsKey(token.Type))
            {
                lexer.TokenBack();
                Expression factor = ParseFactor();

                if (factor == null)
                {
                    return null;
                }

                return ParseSlice(factor) ?? factor;
            }

            unOpType = unaryOperationToUnaryOperationType[token.Type];
            expression = ParseUnaryOperation();
            return new UnaryOperation(unOpType, expression, new Position(startPosition, token.Position));
        }

        private Expression ParseSlice(Expression slicedExpression)
        {
            Expression index = ParseIndexer();

            return index == null ? null : new Slice(slicedExpression, index, new Position(slicedExpression.Position, token.Position));
        }

        private Expression ParseIndexer()
        {
            token = lexer.GetNextTokenForParser();
            Position startPosition = token.Position;

            if (token.Type != TokenType.OpenBracket)
            {
                lexer.TokenBack();
                return null;
            }

            Expression index = ParseExpression();
            if (index == null)
            {
                throw new ParserException(ParserException.ExceptionType.IncorrectExpression,
                    new Position(startPosition, token.Position));
            }

            if (token.Type != TokenType.CloseBracket)
            {
                throw new ParserException(ParserException.ExceptionType.MissingCloseBracket,
                    lexer.GetPrevToken().GetNextPosition());
            }

            token = lexer.GetNextTokenForParser();
            return index;
        }

        private Expression ParseFactor()
        {
            Expression parseResult;
            token = lexer.GetNextTokenForParser();

            switch (token.Type)
            {
                case TokenType.Number:
                    parseResult = new Number(token.Name, token.Position);
                    break;
                case TokenType.OpenParenthesis:
                    parseResult = ParseExpression();

                    if (token.Type != TokenType.CloseParenthesis)
                    {
                        throw new ParserException(ParserException.ExceptionType.MissingCloseParenthesis, lexer.GetPrevToken().Position);
                    }

                    break;
                case TokenType.String:
                    parseResult = new StringConstant(token.Name, token.Position);
                    break;
                case TokenType.Identifier:
                    if (methodOperationToMethodOperationType.ContainsKey(token.Name))
                    {
                        Position startPosition = token.Position;
                        Token curOperationToken = token;
                        token = lexer.GetNextTokenForParser();

                        if (token.Type != TokenType.OpenParenthesis)
                        {
                            throw new ParserException(ParserException.ExceptionType.MissingOpenParenthesis, lexer.GetPrevToken().Position);
                        }

                        parseResult = ParseExpression();

                        if (token.Type != TokenType.CloseParenthesis)
                        {
                            throw new ParserException(ParserException.ExceptionType.MissingCloseParenthesis, lexer.GetPrevToken().Position);
                        }

                        return new MethodOperation(methodOperationToMethodOperationType[curOperationToken.Name], parseResult,
                            new Position(startPosition, token.Position));
                    }

                    return new Variable(token.Name, token.Position);
                default:
                    throw new ParserException(ParserException.ExceptionType.IncorrectExpression, token.Position);
            }
            return parseResult;
        }

        public List<IError> GetErrors()
        {
            return errors;
        }

        private bool IsKeyWord(string word)
        {
            return keyWords.Contains(word);
        }
    }
}