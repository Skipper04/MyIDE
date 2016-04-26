using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using Interpreter.Exceptions;

namespace Interpreter.Values
{
    public abstract class Value : IEquatable<Value>
    {
        protected enum BinaryOperator
        {
            Plus,
            Minus,
            Multiply,
            Divide,
            Degree
        }

        protected enum BoolOperator
        {
            Equal,
            NotEqual,
            Less,
            LessOrEqual,
            Greater,
            GreaterOrEqual
        }

        //Used for switch
        public ValueType Type { get; private set; }

        protected Value(ValueType type)
        {
            Type = type;
        }

        public abstract void Set(Value value);

        public virtual Value Add(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Value Add()
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Value Substract(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Value Substract()
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Value Multiply(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Value Divide(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Value Pow(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Double Sin()
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Double Cos()
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Bool IsLess(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Bool IsLessOrEqual(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Bool IsEqual(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Bool IsNotEqual(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Bool IsGreaterOrEqual(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Bool IsGreater(Value value)
        {
            throw new ValueException(ValueException.ExceptionType.CannotCalculate);
        }

        public virtual Value this[Value index]
        {
            get
            {
                throw new ValueException(ValueException.ExceptionType.InvalidIndexing, Type);
            }
            set
            {
                throw new ValueException(ValueException.ExceptionType.InvalidIndexing, Type);
            }
        }

        /// <summary>
        /// Checks is Value null. If true, throws ArgumentNullException
        /// </summary>
        protected void CheckValueToNull(Object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
        }

        public abstract bool Equals(Value other);
    }
}
