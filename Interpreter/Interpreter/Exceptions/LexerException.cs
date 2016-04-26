using System;
using System.Threading;

namespace Interpreter.Exceptions
{
    public class LexerException : BaseException
    {
        public ExceptionType ExType { get; private set; }

        public override string GetMessage()
        {
            switch (ExType)
            {
                case ExceptionType.PreviousTokenDoesNotExist:
                    return "Previous token does not exist";
                case ExceptionType.BadCompileConstantValue:
                    return "Bad compile constant value";
                case ExceptionType.UnknownToken:
                    return "Unknown code part";
                default:
                    return string.Empty;
            }
        }

        public enum ExceptionType
        {
            PreviousTokenDoesNotExist,
            //IncorrectSymbol,
            BadCompileConstantValue,
            UnknownToken
        }

        public LexerException(ExceptionType exType, Position position)
            : base(position)
        {
            ExType = exType;
        }
    }
}
