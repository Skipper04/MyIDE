using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Values;

namespace Interpreter.Ast
{
    class UnaryOperation : Expression
    {
        private Parser.UnaryOperationType type;
        private Expression expr;

        public UnaryOperation(Parser.UnaryOperationType type, 
            Expression expression, Position position) : base(position)
        {
            if (expression == null)
            {
                throw new ArgumentNullException();
            }

            this.type = type;
            expr = expression;
        }

        public override Value Calculate(Context context)
        {
            Value exprResult = expr.Calculate(context);
            switch (type)
            {
                case Parser.UnaryOperationType.Plus:
                    return exprResult.Add();
                case Parser.UnaryOperationType.Minus:
                    return exprResult.Substract();
                default:
                    throw new NotImplementedException("Unrecognized UnaryOperationType value.");
            }
        }
    }
}
