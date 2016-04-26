using System;

namespace Interpreter.Ast
{
    public abstract class Node
    {
        public Position Position { get; protected set; }

        protected Node(Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException();
            }

            Position = position;
        }
    }
}
