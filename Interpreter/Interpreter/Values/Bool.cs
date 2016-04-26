using System;

namespace Interpreter.Values
{
    public class Bool : Value
    {
        private const ValueType boolType = ValueType.Bool;
        public bool BoolValue { get; private set; }

        public Bool()
            : base(boolType)
        {
        }

        public Bool(bool value)
            : base(boolType)
        {
            BoolValue = value;
        }

        public Bool(Bool value)
            : base(boolType)
        {
            BoolValue = value.BoolValue;
        }

        public override void Set(Value value)
        {
            throw new NotImplementedException();
        }

        public override Bool IsEqual(Value value)
        {
            if (value is Bool)
            {
                return new Bool(BoolValue.Equals(((Bool)value).BoolValue));
            }
            throw new Exception("Bad type");
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(Value other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return BoolValue ? "true" : "false";
        }
    }
}
