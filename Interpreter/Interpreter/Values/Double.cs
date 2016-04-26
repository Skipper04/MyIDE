using System;
using System.Globalization;
using Interpreter.Exceptions;

namespace Interpreter.Values
{
    public sealed class Double : Value
    {
        private const ValueType doubleType = ValueType.Double;
        private const double inaccuracy = 1e-7;
        public double DoubleValue { get; private set; }

        public Double() : base(doubleType)
        {
        }

        public Double(double value)
            : base(doubleType)
        {
            DoubleValue = value;
        }

        public override void Set(Value value)
        {
            CheckValueToNull(value);

            switch (value.Type)
            {
                case ValueType.Double:
                    DoubleValue = ((Double)value).DoubleValue;
                    break;
                case ValueType.Int:
                    DoubleValue = ((Int)value).IntValue;
                    break;
                default:
                    throw new ValueException(ValueException.ExceptionType.CannotConvert, value.Type, doubleType);
            }
        }

        public override Value Add(Value value)
        {
            return GetValue(value, BinaryOperator.Plus);
        }

        public override Value Add()
        {
            return new Double(DoubleValue);
        }

        public override Value Substract()
        {
            return new Double(-DoubleValue);
        }

        public override Value Substract(Value value)
        {
            return GetValue(value, BinaryOperator.Minus);
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

        public override Double Sin()
        {
            return new Double(Math.Sin(DoubleValue));
        }

        public override Double Cos()
        {
            return new Double(Math.Cos(DoubleValue));
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
                case ValueType.Int:
                    double rightDouble = value.Type == ValueType.Double
                        ? ((Double)value).DoubleValue
                        : ((Int)value).IntValue;
                    switch (op)
                    {
                        case BoolOperator.Less:
                            return new Bool(IsLess(DoubleValue, rightDouble));
                        case BoolOperator.LessOrEqual:
                            return new Bool(IsLess(DoubleValue, rightDouble) || IsEqual(DoubleValue, rightDouble));
                        case BoolOperator.Equal:
                            return new Bool(IsEqual(DoubleValue, rightDouble));
                        case BoolOperator.GreaterOrEqual:
                            return new Bool(IsGreater(DoubleValue, rightDouble) || IsEqual(DoubleValue, rightDouble));
                        case BoolOperator.Greater:
                            return new Bool(IsGreater(DoubleValue, rightDouble));
                        case BoolOperator.NotEqual:
                            return new Bool(!IsEqual(DoubleValue, rightDouble));
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
                case ValueType.Double:
                case ValueType.Int:
                    double rightDouble = value.Type == ValueType.Double
                    ? ((Double)value).DoubleValue
                    : ((Int)value).IntValue;
                    switch (op)
                    {
                        case BinaryOperator.Plus:
                            return new Double(DoubleValue + rightDouble);
                        case BinaryOperator.Minus:
                            return new Double(DoubleValue - rightDouble);
                        case BinaryOperator.Multiply:
                            return new Double(DoubleValue * rightDouble);
                        case BinaryOperator.Divide:
                            return new Double(DoubleValue / rightDouble);
                        case BinaryOperator.Degree:
                            return new Double(Math.Pow(DoubleValue, rightDouble));
                        default:
                            throw new NotSupportedException();
                    }
                default:
                    throw new ValueException(ValueException.ExceptionType.CannotCalculate);
            }
        }

        private static bool IsLess(double first, double second)
        {
            return first + inaccuracy < second;
        }

        private static bool IsEqual(double first, double second)
        {
            return Math.Abs(first - second) < inaccuracy;
        }

        private static bool IsGreater(double first, double second)
        {
            return second + inaccuracy < first;
        }

        public override string ToString()
        {
            return DoubleValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}
