using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Interpreter.Exceptions;
using Interpreter.Interfaces;

namespace Interpreter
{
    public class Lexer
    {
        private enum NumberState
        {
            Error,
            Start,
            Significand,
            Separator,
            DigitAfterSeparator,
            Exponent,
            ExponentSign,
            ExponentDigit
        };

        #region Lexer constants

        private const char newLineChar = '\n';
        private const char carriageReturnChar = '\r';
        private const char tabChar = '\t';
        private const string alphaVariableChars = "_ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const char plus = '+';
        private const char minus = '-';
        private const char multiply = '*';
        private const char divide = '/';
        private const char semicolon = ';';
        private const char assignment = '=';
        private const char openParenthesis = '(';
        private const char closeParenthesis = ')';
        private const char openCurlyBrace = '{';
        private const char closeCurlyBrace = '}';
        private const char openBracket = '[';
        private const char closeBracket = ']';
        private const char quotationMark = '"';
        private const char less = '<';
        private const char greater = '>';
        private const char exclamationMark = '!';
        private const int startTokenIndex = -1;
        private const int columnIndexAfterNewLineChar = -1;
        private const char decimalSeparator = '.';
        private const char exponent = 'e';
        private const char colon = ':';

        private readonly Dictionary<string, TokenType> keywordToTokenType =
            new Dictionary<string, TokenType>()
            {
                {"if", TokenType.If},
                {"while", TokenType.While},
                {"for", TokenType.For},
                {"goto", TokenType.Goto},
                {"else", TokenType.Else}
            };

        #endregion

        private readonly List<Token> tokens = new List<Token>();
        private int curTokenIndex = startTokenIndex;
        private readonly string programText;
        private int line;
        private int column;
        private int curIndex;
        private readonly List<IError> errors;

        public Lexer(string programText)
            : this(programText, new List<IError>())
        {
        }

        public Lexer(string programText, List<IError> errors)
        {
            if (programText == null || errors == null)
            {
                throw new ArgumentNullException();
            }

            this.programText = programText;
            this.errors = errors;
        }

        public Lexer(string programText, int currentPos)
        {
            if (string.IsNullOrEmpty(programText))
            {
                throw new ArgumentNullException();
            }

            if (currentPos < 0 || currentPos > programText.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.programText = programText;
            curIndex = currentPos;
        }

        private void Shift()
        {
            curIndex++;
            column++;
        }

        private static bool IsFinalNumberState(NumberState state)
        {
            switch (state)
            {
                case NumberState.Significand:
                case NumberState.DigitAfterSeparator:
                case NumberState.ExponentDigit:
                    return true;
                default:
                    return false;
            }
        }

        private void SkipSpaces()
        {
            while (curIndex < programText.Length &&
                   (programText[curIndex] == newLineChar || programText[curIndex] == tabChar
                    || programText[curIndex] == carriageReturnChar || char.IsSeparator(programText[curIndex])))
            {
                if (programText[curIndex] == newLineChar)
                {
                    line++;
                    column = columnIndexAfterNewLineChar;
                }

                Shift();
            }
        }

        public void SkipTokensUntilToken(params TokenType[] tokenTypes)
        {
            Token token = Peek();
            while (token.Type != TokenType.Eof && !tokenTypes.Contains(token.Type))
            {
                token = GetNextToken();
            }
        }

        public Token Peek()
        {
            return tokens[curTokenIndex];
        }

        private Position GetCurrentTokenPosition(int tokenLength)
        {
            return new Position(line, column - tokenLength, curIndex - tokenLength, tokenLength);
        }

        private Token AddToken(Token token)
        {
            tokens.Add(token);
            curTokenIndex++;
            return tokens[curTokenIndex];
        }

        public void TokenBack()
        {
            curTokenIndex--;
            if (curTokenIndex < startTokenIndex)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private bool IsEndOfProgramText(int index)
        {
            return index >= programText.Length;
        }

        public Token GetPrevToken()
        {
            if (curTokenIndex - 1 < startTokenIndex)
            {
                throw new IndexOutOfRangeException();
            }

            return tokens[curTokenIndex - 1];
        }

        public Token GetNextTokenForParser()
        {
            Token token = GetNextToken();
            if (token.Type == TokenType.Unknown)
            {
                throw new LexerException(LexerException.ExceptionType.UnknownToken, token.Position);
            }

            return token;
        }

        public Token GetNextTokenForColorer(int endPos)
        {
            if (curIndex >= endPos)
            {
                return new Token(TokenType.Eof, new Position());
            }
            Token token;
            try
            {
                token = GetNextToken();
            }
            catch (LexerException ex)
            {
                return new Token(TokenType.Unknown, ex.Position);
            }

            return token;
        }

        private Token GetNextToken()
        {
            if (curTokenIndex >= startTokenIndex && curTokenIndex < tokens.Count - 1)
            {
                curTokenIndex++;
                return tokens[curTokenIndex];
            }

            SkipSpaces();

            if (IsEndOfProgramText(curIndex))
            {
                return AddToken(new Token(TokenType.Eof, new Position(line, column, curIndex)));
            }

            switch (programText[curIndex])
            {
                case plus:
                    Shift();
                    return AddToken(new Token(TokenType.Plus, GetCurrentTokenPosition(1)));
                case minus:
                    Shift();
                    return AddToken(new Token(TokenType.Minus, GetCurrentTokenPosition(1)));
                case multiply:
                    Shift();
                    if (IsEndOfProgramText(curIndex) || programText[curIndex] != multiply)
                    {
                        return AddToken(new Token(TokenType.Multiply, GetCurrentTokenPosition(1)));
                    }

                    Shift();
                    return AddToken(new Token(TokenType.Degree, GetCurrentTokenPosition(2)));
                case divide:
                    Shift();
                    return AddToken(new Token(TokenType.Divide, GetCurrentTokenPosition(1)));
                case openBracket:
                    Shift();
                    return AddToken(new Token(TokenType.OpenBracket, GetCurrentTokenPosition(1)));
                case closeBracket:
                    Shift();
                    return AddToken(new Token(TokenType.CloseBracket, GetCurrentTokenPosition(1)));
                case openCurlyBrace:
                    Shift();
                    return AddToken(new Token(TokenType.OpenCurlyBrace, GetCurrentTokenPosition(1)));
                case closeCurlyBrace:
                    Shift();
                    return AddToken(new Token(TokenType.CloseCurlyBrace, GetCurrentTokenPosition(1)));
                case openParenthesis:
                    Shift();
                    return AddToken(new Token(TokenType.OpenParenthesis, GetCurrentTokenPosition(1)));
                case closeParenthesis:
                    Shift();
                    return AddToken(new Token(TokenType.CloseParenthesis, GetCurrentTokenPosition(1)));
                case semicolon:
                    Shift();
                    return AddToken(new Token(TokenType.Semicolon, GetCurrentTokenPosition(1)));
                case colon:
                    Shift();
                    return AddToken(new Token(TokenType.Colon, GetCurrentTokenPosition(1)));
                case assignment:
                    Shift();

                    if (IsEndOfProgramText(curIndex) || programText[curIndex] != assignment)
                    {
                        return AddToken(new Token(TokenType.Assignment, GetCurrentTokenPosition(1)));
                    }

                    Shift();
                    return AddToken(new Token(TokenType.Equal, GetCurrentTokenPosition(2)));
                case exclamationMark:
                    Shift();

                    if (IsEndOfProgramText(curIndex) || programText[curIndex] != assignment)
                    {
                        return AddToken(new Token(TokenType.Negation, GetCurrentTokenPosition(1)));
                    }

                    Shift();
                    return AddToken(new Token(TokenType.NotEqual, GetCurrentTokenPosition(2)));
                case less:
                    Shift();

                    if (IsEndOfProgramText(curIndex) || programText[curIndex] != assignment)
                    {
                        return AddToken(new Token(TokenType.Less, GetCurrentTokenPosition(1)));
                    }

                    Shift();
                    return AddToken(new Token(TokenType.LessOrEqual, GetCurrentTokenPosition(2)));
                case greater:
                    Shift();

                    if (IsEndOfProgramText(curIndex) || programText[curIndex] != assignment)
                    {
                        return AddToken(new Token(TokenType.Greater, GetCurrentTokenPosition(1)));
                    }

                    Shift();
                    return AddToken(new Token(TokenType.GreaterOrEqual, GetCurrentTokenPosition(2)));
            }

            int columnBeforeParsing = column;
            int indexBeforeParsing = curIndex;

            string number;
            if (TryParseNumber(out number))
            {
                return AddToken(new Token(TokenType.Number, number, GetCurrentTokenPosition(number.Length)));
            }

            column = columnBeforeParsing;
            curIndex = indexBeforeParsing;

            string stringLiteral;
            if (TryParseString(out stringLiteral))
            {
                return AddToken(new Token(TokenType.String, stringLiteral,
                    GetCurrentTokenPosition(stringLiteral.Length + 2)));
            }

            column = columnBeforeParsing;
            curIndex = indexBeforeParsing;

            string identifier;
            if (!TryParseIdentifier(out identifier))
            {
                AddToken(new Token(TokenType.Unknown, identifier, new Position(line, column, curIndex, identifier.Length)));
                curIndex++;
                return tokens[curTokenIndex];
            }

            if (keywordToTokenType.ContainsKey(identifier))
            {
                return AddToken(new Token(keywordToTokenType[identifier],
                    GetCurrentTokenPosition(identifier.Length)));
            }

            return AddToken(new Token(TokenType.Identifier, identifier,
                GetCurrentTokenPosition(identifier.Length)));
        }

        private bool TryParseString(out string stringLiteral)
        {
           int startIndex = curIndex;

            if (programText[curIndex] != quotationMark)
            {
                stringLiteral = null;
                return false;
            }

            Shift();

            while (!IsEndOfProgramText(curIndex) && programText[curIndex] != quotationMark
                   && programText[curIndex] != newLineChar)
            {
                Shift();
            }

            if (IsEndOfProgramText(curIndex) || programText[curIndex] != quotationMark)
            {
                throw new LexerException(LexerException.ExceptionType.BadCompileConstantValue,
                    GetCurrentTokenPosition(curIndex - startIndex));
            }

            stringLiteral = programText.Substring(startIndex + 1, curIndex - startIndex - 1);
            Shift();
            return true;
        }

        private bool TryParseIdentifier(out string identifier)
        {
            if (!alphaVariableChars.Contains(programText[curIndex].ToString()))
            {
                identifier = programText[curIndex].ToString();
                return false;
            }

            int startIndex = curIndex;
            while (!IsEndOfProgramText(curIndex) && (alphaVariableChars.Contains(programText[curIndex].ToString())
                || char.IsDigit(programText[curIndex])))
            {
                Shift();
            }


            identifier = programText.Substring(startIndex, curIndex - startIndex);
            return true;
        }

        private static NumberState GetNextNumberState(NumberState state, char symbol)
        {
            switch (state)
            {
                case NumberState.Start:
                    if (char.IsDigit(symbol))
                    {
                        return NumberState.Significand;
                    }
                    return symbol == decimalSeparator ? NumberState.Separator : NumberState.Error;
                case NumberState.Significand:
                    if (char.IsDigit(symbol))
                    {
                        return NumberState.Significand;
                    }
                    if (char.ToLower(symbol).Equals(exponent))
                    {
                        return NumberState.Exponent;
                    }
                    return symbol == decimalSeparator ? NumberState.Separator : NumberState.Error;
                case NumberState.Separator:
                    return char.IsDigit(symbol) ? NumberState.DigitAfterSeparator : NumberState.Error;
                case NumberState.DigitAfterSeparator:
                    if (char.ToLower(symbol).Equals(exponent))
                    {
                        return NumberState.Exponent;
                    }
                    return char.IsDigit(symbol) ? NumberState.DigitAfterSeparator : NumberState.Error;
                case NumberState.Exponent:
                    if (symbol == minus || symbol == plus)
                    {
                        return NumberState.ExponentSign;
                    }
                    return char.IsDigit(symbol) ? NumberState.ExponentDigit : NumberState.Error;
                case NumberState.ExponentSign:
                    return char.IsDigit(symbol) ? NumberState.ExponentDigit : NumberState.Error;
                case NumberState.ExponentDigit:
                    return char.IsDigit(symbol) ? NumberState.ExponentDigit : NumberState.Error;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private bool TryParseNumber(out string number)
        {
            StringBuilder numberBuilder = new StringBuilder();
            NumberState prevState = NumberState.Start;
            NumberState curState = NumberState.Start;

            while (!IsEndOfProgramText(curIndex))
            {
                curState = GetNextNumberState(curState, programText[curIndex]);
                if (curState == NumberState.Error)
                {
                    break;
                }
                numberBuilder.Append(programText[curIndex]);
                prevState = curState;
                Shift();
            }

            if (IsFinalNumberState(prevState))
            {
                number = numberBuilder.ToString();
                return true;
            }

            number = null;
            return false;
        }
    }
}
