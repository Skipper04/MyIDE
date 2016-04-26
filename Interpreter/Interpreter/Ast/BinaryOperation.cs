using System;
using Interpreter.Values;
using Double = Interpreter.Values.Double;

namespace Interpreter.Ast
{
    public class BinaryOperation : Expression
    {
        private Parser.BinaryOperationType type;
        private Expression left;
        private Expression right;

        public BinaryOperation(Parser.BinaryOperationType type, Expression left, Expression right, 
            Position position) : base(position)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            this.type = type;
            this.left = left;
            this.right = right;
        }

        public override Value Calculate(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            switch (type)
            {
                case Parser.BinaryOperationType.Plus:
                    return left.Calculate(context).Add(right.Calculate(context));
                case Parser.BinaryOperationType.Minus:
                    return left.Calculate(context).Substract(right.Calculate(context));
                case Parser.BinaryOperationType.Multiply:
                    return left.Calculate(context).Multiply(right.Calculate(context));
                case Parser.BinaryOperationType.Divide:
                    return left.Calculate(context).Divide(right.Calculate(context));
                case Parser.BinaryOperationType.Power:
                    return left.Calculate(context).Pow(right.Calculate(context));
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
