using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Exceptions
{
    public class ParserException : BaseException
    {
        public ExceptionType ExType { get; private set; }

        public enum ExceptionType
        {
            MissingSemicolon,
            MissingCloseBlockBracket,
            UnknownCode,
            IncorrectStatement,
            MissingOpenParenthesis,
            MissingCondition,
            MissingCloseParenthesis,
            MissingCloseBracket,
            MissingVariable,
            MissingLabel,
            MissingCompareOperator,
            IdentifierIsKeyWord,
            LableDuplicate,
            LableNotFound,
            IncorrectNameOfVariable,
            MissingAssignment,
            MissingDeclarationOrAssignment,
            IncorrectExpression,
            MissingType,
            MissingIndexer
        }

        public ParserException(ExceptionType exType, Position position)
            : base(position)
        {
            ExType = exType;
        }

        public override string GetMessage()
        {
            switch (ExType)
            {
                case ExceptionType.MissingSemicolon:
                    return "Expected ;";
                case ExceptionType.MissingCloseBlockBracket:
                    return "Expected }";
                case ExceptionType.UnknownCode:
                    return "Unknown code part";
                case ExceptionType.IncorrectStatement:
                    return "Expected statement";
                case ExceptionType.MissingOpenParenthesis:
                    return "Expected (";
                case ExceptionType.MissingCondition:
                    return "Expected condition";
                case ExceptionType.MissingCloseParenthesis:
                    return "Expected )";
                case ExceptionType.MissingVariable:
                    return "Expected variable";
                case ExceptionType.MissingLabel:
                    return "Expected label";
                case ExceptionType.MissingCompareOperator:
                    return "Expected compare operator";
                case ExceptionType.IdentifierIsKeyWord:
                    return "Identifier is a key word";
                case ExceptionType.LableDuplicate:
                    return "Lable duplicate";
                case ExceptionType.LableNotFound:
                    return "Lable not found";
                case ExceptionType.IncorrectNameOfVariable:
                    return "Incorrect variable name";
                case ExceptionType.MissingAssignment:
                    return "Expected Assignment";
                case ExceptionType.MissingDeclarationOrAssignment:
                    return "Expected declaration or assignment";
                case ExceptionType.IncorrectExpression:
                    return "Incorrect expression";
                case ExceptionType.MissingCloseBracket:
                    return "Expeceted ]";
                case ExceptionType.MissingType:
                    return "Usage of type expected";
                case ExceptionType.MissingIndexer:
                    return "Expected [numberOfElents]";
                default:
                    return string.Empty;
            }
        }
    }
}
