using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Values
{
    public class Null : Value
    {
        private const string nullToString = "null";
        private const ValueType nullType = ValueType.Null;
        
        public Null(ValueType type)
            : base(type)
        {
        }

        public Null()
            : base(nullType)
        {
        }

        public override void Set(Value value)
        {
            CheckValueToNull(value);

            Value leftValue = ValueHelper.CreateValue(Type);
            leftValue.Set(value);
        }

        public override bool Equals(Value other)
        {
            return other is Null;
        }

        public override string ToString()
        {
            return nullToString;
        }
    }
}
