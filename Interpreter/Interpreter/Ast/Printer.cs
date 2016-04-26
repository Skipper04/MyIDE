using System;
using System.Collections.Generic;

namespace Interpreter.Ast
{
    internal sealed class Printer : Statement
    {
        public Expression Expression { get; private set; }

        public Printer(Expression expression, Position position)
            : base(position)
        {
            if (expression == null)
            {
                throw new ArgumentNullException();
            }
            
            Expression = expression;
        }
    }
}
