using System;
using Interpreter.Values;
using ValueType = Interpreter.ValueType;

namespace Interpreter.Ast
{
    class Declaration : Statement
    {
        public ValueType Type { get; private set; }
        public ValueType InternalType { get; private set; }
        public Variable Destination { get; private set; }
        public Expression Source { get; private set; }

        public Declaration(ValueType type, ValueType internalType, Variable destination, Expression  source, Position position) : base(position)
        {
            if (destination == null)
            {
                throw new ArgumentNullException();
            }

            Type = type;
            InternalType = internalType;
            Destination = destination;
            Source = source;
        }

        public Declaration(ValueType type,  Variable destination, Expression source, Position position)
            : this(type, ValueType.Null, destination, source, position)
        {
        }
    }
}
