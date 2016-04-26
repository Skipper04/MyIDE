using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Exceptions;
using Interpreter.Values;
using Array = Interpreter.Values.Array;

namespace Interpreter.Ast
{
    class ArrayInitializer : Expression
    {
        public readonly Expression NumberOfElements;
        public readonly ValueType InternalType;

        public ArrayInitializer(Expression numberOfElements, ValueType internalType, Position position) : base(position)
        {
            NumberOfElements = numberOfElements;
            InternalType = internalType;
        }

        public override Value Calculate(Context context)
        {
            Value numberOfElementsValue = NumberOfElements.Calculate(context);
            Int numberOfElements = numberOfElementsValue as Int;
            if (numberOfElements == null)
            {
                throw new ValueException(ValueException.ExceptionType.CannotConvert,
                    numberOfElementsValue.Type, ValueType.Int);
            }

            return new Array(InternalType, numberOfElements.IntValue);
        }
    }
}
