using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Exceptions
{
    public class InterpreterException : BaseException
    {
        public ExceptionType ExType { get; private set; }

        public enum ExceptionType
        {
            NotDeclaredVariable,
            DivideByZero,
            CannotConvert,
            AlreadyHasBeenDeclaredVariable,
            UnknownStatement
        }

        public InterpreterException(ExceptionType exType, Position position)
            : base(position)
        {
            ExType = exType;
        }

        public override string GetMessage()
        {
            switch (ExType)
            {
                case ExceptionType.NotDeclaredVariable:
                    return "Variabe was not delared";
                case ExceptionType.DivideByZero:
                    return "Divide by zero";
                case ExceptionType.CannotConvert:
                    return "Cannot convert";
                case ExceptionType.AlreadyHasBeenDeclaredVariable:
                    return "Variable already has been declared";
                case ExceptionType.UnknownStatement:
                    return "Unknown statement";
                default:
                    return string.Empty;
            }
        }
    }
}
