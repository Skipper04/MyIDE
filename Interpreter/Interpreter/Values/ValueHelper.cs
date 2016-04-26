using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Values
{
    public static class ValueHelper
    {
        public static Value CreateValue(ValueType type, ValueType internalType = ValueType.Null)
        {
            switch (type)
            {
                case ValueType.Bool:
                    return new Bool();
                case ValueType.Double:
                    return new Double();
                case ValueType.Int:
                    return new Int();
                case ValueType.String:
                    return new String();
                case ValueType.Array:
                    if (internalType == ValueType.Null)
                    {
                        throw new ArgumentException("Invalid internal type");
                    }
                    return new Array(internalType);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
