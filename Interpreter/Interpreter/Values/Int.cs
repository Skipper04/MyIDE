using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Exceptions;
using Interpreter.Ast;

namespace Interpreter.Values
{
    public class Int : Value
    {

        private const ValueType intType = ValueType.Int;
        public int IntValue { get; private set; }

        public Int()
            : base(intType)
        {
        }

        public Int(int value)
            : base(intType)
        {
            IntValue = value;
        }


        public override void Set(Value value)
        {
            CheckValueToNull(value);

            switch (value.Type)
            {
                case ValueType.Int:
                    IntValue = ((Int)value).IntValue;
                    break;
                default:
                    throw new ValueException(ValueException.ExceptionType.CannotConvert, value.Type, intType);
            }
        }

        public override Value Add(Value value)
        {
            return GetValue(value, BinaryOperator.Plus);
        }

        public override Value Add()
        {
            return new Int(IntValue);
        }

        public override Value Substract(Value value)
        {
            return GetValue(value, BinaryOperator.Minus);
        }

        public override Value Substract()
        {
            return new Int(-IntValue);
        }

        public override Value Multiply(Value value)
        {
            return GetValue(value, BinaryOperator.Multiply);
        }

        public override Value Divide(Value value)
        {
            return GetValue(value, BinaryOperator.Divide);
        }

        public override Value Pow(Value value)
        {
            return GetValue(value, BinaryOperator.Degree);
        }

        public override Bool IsLess(Value value)
        {
            return GetBoolValue(value, BoolOperator.Less);
        }

        public override Bool IsLessOrEqual(Value value)
        {
            return GetBoolValue(value, BoolOperator.LessOrEqual);
        }

        public override Bool IsEqual(Value value)
        {
            return GetBoolValue(value, BoolOperator.Equal);
        }

        public override Bool IsNotEqual(Value value)
        {
            return GetBoolValue(value, BoolOperator.NotEqual);
        }

        public override Bool IsGreaterOrEqual(Value value)
        {
            return GetBoolValue(value, BoolOperator.GreaterOrEqual);
        }

        public override Bool IsGreater(Value value)
        {
            return GetBoolValue(value, BoolOperator.Greater);
        }

        public override bool Equals(Value other)
        {
            return IsEqual(other).BoolValue;
        }

        private Bool GetBoolValue(Value value, BoolOperator op)
        {
            CheckValueToNull(value);

            switch (value.Type)
            {
                case ValueType.Double:
                    switch (op)
                    {
                        case BoolOperator.Less:
                            return new Bool(value.IsGreater(this));
                        case BoolOperator.LessOrEqual:
                            return new Bool(value.IsGreaterOrEqual(this));
                        case BoolOperator.Equal:
                            return new Bool(value.IsEqual(this));
                        case BoolOperator.GreaterOrEqual:
                            return new Bool(value.IsLessOrEqual(this));
                        case BoolOperator.Greater:
                            return new Bool(value.IsLess(this));
                        default:
                            throw new NotSupportedException();
                    }
                case ValueType.Int:
                    Int val = value as Int;
                    if (val == null)
                    {
                        throw new ValueException(ValueException.ExceptionType.CannotConvert);
                    }
                    switch (op)
                    {
                        case BoolOperator.Less:
                            return new Bool(IntValue < val.IntValue);
                        case BoolOperator.LessOrEqual:
                            return new Bool(IntValue <= val.IntValue);
                        case BoolOperator.Equal:
                            return new Bool(IntValue == val.IntValue);
                        case BoolOperator.GreaterOrEqual:
                            return new Bool(IntValue >= val.IntValue);
                        case BoolOperator.Greater:
                            return new Bool(IntValue > val.IntValue);
                        case BoolOperator.NotEqual:
                            return new Bool(IntValue != val.IntValue);
                        default:
                            throw new NotSupportedException();
                    }

                default:
                    throw new ValueException(ValueException.ExceptionType.CannotCalculate);
            }
        }

        private Value GetValue(Value value, BinaryOperator op)
        {
            CheckValueToNull(value);

            switch (value.Type)
            {
                case ValueType.Int:
                    Int val = value as Int;
                    if (val == null)
                    {
                        throw new ValueException(ValueException.ExceptionType.CannotConvert);
                    }
                    switch (op)
                    {
                        case BinaryOperator.Plus:
                            return new Int(IntValue + val.IntValue);
                        case BinaryOperator.Minus:
                            return new Int(IntValue - val.IntValue);
                        case BinaryOperator.Multiply:
                            return new Int(IntValue * val.IntValue);
                        case BinaryOperator.Divide:
                            if (val.IntValue == 0)
                            {
                                throw new ValueException(ValueException.ExceptionType.DivideByZero);
                            }

                            return new Int(IntValue / val.IntValue);
                        case BinaryOperator.Degree:
                            return new Int((int)Math.Pow(IntValue, val.IntValue));
                        default:
                            throw new NotSupportedException();
                    }
                case ValueType.Double:
                    double rightDouble = ((Double)value).DoubleValue;
                    switch (op)
                    {
                        case BinaryOperator.Plus:
                            return new Double(IntValue + rightDouble);
                        case BinaryOperator.Minus:
                            return new Double(IntValue - rightDouble);
                        case BinaryOperator.Multiply:
                            return new Double(IntValue * rightDouble);
                        case BinaryOperator.Divide:
                            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
                        case BinaryOperator.Degree:
                            return new Double(Math.Pow(IntValue, rightDouble));
                        default:
                            throw new NotSupportedException();
                    }
                default:
                    throw new ValueException(ValueException.ExceptionType.CannotCalculate);
            }
        }

        public override string ToString()
        {
            return IntValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}
