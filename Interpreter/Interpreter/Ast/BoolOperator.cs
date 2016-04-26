using System;
using Interpreter.Values;

namespace Interpreter.Ast
{
    public class BoolOperator : Node
    {
        private readonly Expression left;
        private readonly Expression right;
        private readonly Parser.CompareOperatorType compareOperator;

        public BoolOperator(Parser.CompareOperatorType compareOperator, Expression left, 
            Expression right, Position position) : base(position)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            this.compareOperator = compareOperator;
            this.left = left;
            this.right = right;
        }

        public Bool Calculate(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            switch (compareOperator)
            {
                case Parser.CompareOperatorType.Less:
                    return left.Calculate(context).IsLess(right.Calculate(context));
                case Parser.CompareOperatorType.LessOrEqual:
                    return left.Calculate(context).IsLessOrEqual(right.Calculate(context));
                case Parser.CompareOperatorType.Equal:
                    return left.Calculate(context).IsEqual(right.Calculate(context));
                case Parser.CompareOperatorType.NotEqual:
                    return left.Calculate(context).IsNotEqual(right.Calculate(context));
                case Parser.CompareOperatorType.GreaterOrEqual:
                    return left.Calculate(context).IsGreaterOrEqual(right.Calculate(context));
                case Parser.CompareOperatorType.Greater:
                    return left.Calculate(context).IsGreater(right.Calculate(context));
                default:
                    throw new NotSupportedException("Unknown bool operator");
            }
        }
    }
}
