using System;

namespace Interpreter.Ast
{
    public class Assignment : Statement
    {
        public Expression Destination { get; private set; }
        public Expression Source { get; private set; }

        public Assignment(Expression destination, Expression source, Position position) : base(position)
        {
            if (destination == null || source == null)
            {
                throw new ArgumentNullException();
            }

            if (!(destination is Variable || destination is Slice))
            {
                throw new ArgumentException("The left-hand side of an assignment must be a variable or indexer");
            }

            Destination = destination;
            Source = source;
        }
    }
}
