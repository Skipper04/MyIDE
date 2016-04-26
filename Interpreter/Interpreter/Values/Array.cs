using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Exceptions;

namespace Interpreter.Values
{
    public class Array : Value
    {
        private const ValueType arrayType = ValueType.Array;
        public Value[] ArrayValue { get; private set; }
        public ValueType InternalType { get; private set; }

        public Array(ValueType internalType) : base(arrayType)
        {
            InternalType = internalType;
            ArrayValue = new Value[0];
        }

        public Array(ValueType internalType, int size)
            : this(internalType)
        {
            ArrayValue = new Value[size];

            for (int i = 0; i < size; i++)
            {
                ArrayValue[i] = ValueHelper.CreateValue(internalType);
            }
        }

        public override void Set(Value value)
        {
            Array arrayValue = value as Array;

            if (arrayValue == null)
            {
                throw new ValueException(ValueException.ExceptionType.CannotConvert,
                    value.Type, arrayType);
            }
            
            if (InternalType != arrayValue.InternalType)
            {
                throw new ValueException(ValueException.ExceptionType.CannotConvert,
                    arrayValue.InternalType, InternalType);
            }

            ArrayValue = arrayValue.ArrayValue;
        }

        public override bool Equals(Value other)
        {
            Array array = other as Array;
            if (array == null || array.ArrayValue.Length != ArrayValue.Length || array.InternalType != InternalType)
            {
                return false;
            }

            for (int i = 0; i < array.ArrayValue.Length; i++)
            {
                if (array.ArrayValue[i].IsNotEqual(ArrayValue[i]).BoolValue)
                {
                    return false;
                }
            }

            return true;
        }

        public override Value this[Value indexValue]
        {
            get
            {
                int index = GetIndex(indexValue);
                
                if (index < 0 || index >= ArrayValue.Length)
                {
                    throw new ValueException(ValueException.ExceptionType.IndexOutOfRange);
                }

                return ArrayValue[index];
            }
            set
            {
                int index = GetIndex(indexValue);

                if (index < 0 || index >= ArrayValue.Length)
                {
                    throw new ValueException(ValueException.ExceptionType.IndexOutOfRange);
                }

                ArrayValue[index].Set(value);
            }
        }

        private int GetIndex(Value indexValue)
        {
            Int number = indexValue as Int;
            if (number == null)
            {
                throw new ValueException(ValueException.ExceptionType.CannotConvert,
                    indexValue.Type, ValueType.Int);
            }

            return number.IntValue;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("{ ");
            for (int i = 0; i < ArrayValue.Length; i++)
            {
                if (i != 0)
                {
                    result.Append(", ");
                }
                result.Append(ArrayValue[i]);
            }
            result.Append(" }");

            return result.ToString();
        }
    }
}
