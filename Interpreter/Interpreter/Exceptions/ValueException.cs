using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Exceptions
{
    public class ValueException : Exception
    {
        private readonly ValueType[] valueTypes;
        private readonly ExceptionType exType;

        private readonly Dictionary<ValueType, string> valueTypeToString =
            new Dictionary<ValueType, string>
            {
                {ValueType.Int, "int"},
                {ValueType.Bool, "bool"},
                {ValueType.Double, "double"},
                {ValueType.String, "string"},
                {ValueType.Array, "array"},
            };

        public enum ExceptionType
        {
            CannotConvert,
            CannotCompared,
            DivideByZero,
            InvalidOperator,
            CannotCalculate,
            NotDeclaredEx,
            InvalidIndexing,
            ReadOnlyIndexer,
            IndexOutOfRange
        }

        private readonly Dictionary<ExceptionType, string> exceptionTypeToMessage 
            = new Dictionary<ExceptionType, string>
        {
            {ExceptionType.CannotConvert, "Cannot convert source type {0} to target type {1}"},
            {ExceptionType.CannotCompared, "Cannot compared source type {0} to target type {1}"},
            {ExceptionType.CannotCalculate, "Expression cannot be calculated"},
            {ExceptionType.DivideByZero, "Divide by zero"},
            {ExceptionType.InvalidIndexing, "Cannot apply indexing to an expression of type {0}"},
            {ExceptionType.ReadOnlyIndexer, "Indexer is read only"},
            {ExceptionType.IndexOutOfRange, "Index out of range"}
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exType"></param>
        /// <param name="valueTypes">first = sourse, second - target</param>
        public ValueException(ExceptionType exType, params ValueType[] valueTypes)
        {
            this.exType = exType;
            this.valueTypes = valueTypes;
        }

        public string GetMessage()
        {
            return string.Format(exceptionTypeToMessage[exType], ConvertValueTypesToObjects());
        }

        private object[] ConvertValueTypesToObjects()
        {
            object[] result = new object[valueTypes.Length];

            for (int i = 0; i < valueTypes.Length; i++)
            {
                result[i] = valueTypeToString[valueTypes[i]];
            }

            return result;
        }
    }
}
