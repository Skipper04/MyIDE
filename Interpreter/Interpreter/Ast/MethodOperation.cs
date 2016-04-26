using System;
using Interpreter.Values;
using Double = Interpreter.Values.Double;

namespace Interpreter.Ast
{
    public class MethodOperation : Expression
    {
        private readonly Expression expression;
        private readonly Parser.MethodType type;

        public MethodOperation(Parser.MethodType type, Expression expression, Position position) : base(position)
        {
            if (expression == null)
            {
                throw new ArgumentNullException();
            }

            this.expression = expression;
            this.type = type;
        }

        public override Value Calculate(Context context)
        {
            switch (type)
            {
                case Parser.MethodType.Sin:
                    return expression.Calculate(context).Sin();
                case Parser.MethodType.Cos:
                    return expression.Calculate(context).Cos();
                default:
                    throw new NotSupportedException("Unknown Parser.MethodType");
            }
        }
    }
}
