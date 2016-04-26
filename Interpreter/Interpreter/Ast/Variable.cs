using System;
using Interpreter.Exceptions;
using Interpreter.Values;

namespace Interpreter.Ast
{
    public class Variable : Expression
    {
        public string Name { get; private set; }

        public Variable(string name, Position position) : base(position)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }

            Name = name;
        }

        public override Value Calculate(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            if (!context.VariableValues.ContainsKey(Name))
            {
                throw new InterpreterException(InterpreterException.ExceptionType.NotDeclaredVariable, Position);
            }

            return context.VariableValues[Name];
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
