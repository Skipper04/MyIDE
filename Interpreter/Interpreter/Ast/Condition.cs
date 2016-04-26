using System;
using Interpreter.Values;

namespace Interpreter.Ast
{
    public class Condition : Node
    {
        private readonly BoolOperator boolOperator;

        public Condition(BoolOperator boolOperator, Position position) : base(position)
        {
            this.boolOperator = boolOperator;
        }

        public Bool Calculate(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            return boolOperator.Calculate(context);
        }
    }
}
