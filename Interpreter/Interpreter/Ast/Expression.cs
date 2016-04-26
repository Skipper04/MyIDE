using Interpreter.Values;

namespace Interpreter.Ast
{
    public abstract class Expression : Node
    {
        public abstract Value Calculate(Context context);
        protected Expression(Position position) : base(position)
        {
        }
    }
}
