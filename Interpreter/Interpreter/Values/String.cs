using System;
using Interpreter.Exceptions;

namespace Interpreter.Values
{
    public class String : Value
    {
        private const ValueType stringType = ValueType.String;
        public string StringValue { get; private set; }

        public String()
            : base(stringType)
        {
            StringValue = System.String.Empty;
        }

        public String(string value)
            : base(stringType)
        {
            CheckValueToNull(value);
            StringValue = value;
        }

        public String(String value)
            : base(stringType)
        {
            CheckValueToNull(value);
            StringValue = value.StringValue;
        }

        public override void Set(Value value)
        {
            CheckValueToNull(value);

            switch (value.Type)
            {
                case ValueType.String:
                    StringValue = ((String)value).StringValue;
                    break;
                default:
                    throw new ValueException(ValueException.ExceptionType.CannotConvert, value.Type, stringType);
            }
        }

        public override Value this[Value indexValue]
        {
            get
            {
                Int index = indexValue as Int;
                if (index == null)
                {
                    throw new ValueException(ValueException.ExceptionType.CannotConvert,
                        indexValue.Type, ValueType.Int);
                }

                int stringIndex = index.IntValue;

                if (stringIndex < 0 || stringIndex >= StringValue.Length)
                {
                    throw new ValueException(ValueException.ExceptionType.IndexOutOfRange);
                }

                return new String(StringValue[stringIndex].ToString());
            }
            set
            {
                throw new ValueException(ValueException.ExceptionType.ReadOnlyIndexer);
            }
        }


        public override Value Add(Value value)
        {
            if (!(value is String))
            {
                throw new ValueException(ValueException.ExceptionType.CannotCalculate);
            }

            return new String(StringValue + ((String)value).StringValue);
        }

        public override Bool IsEqual(Value value)
        {
            CheckValueToNull(value);

            if (!(value is String))
            {
                throw new ValueException(ValueException.ExceptionType.CannotCalculate);
            }

            string rightString = ((String)value).StringValue;
            return new Bool(StringValue.Equals(rightString));
        }

        public override Bool IsNotEqual(Value value)
        {
            return new Bool(!IsEqual(value).BoolValue);
        }

        public override bool Equals(Value other)
        {

            return IsEqual(other).BoolValue;
        }

        public override string ToString()
        {
            return StringValue;
        }
    }
}
