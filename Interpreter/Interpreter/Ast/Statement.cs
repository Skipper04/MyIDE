namespace Interpreter.Ast
{
    public abstract class Statement : Node
    {
        public virtual Statement NextStatement { get; set; }

        protected Statement(Position position) : base(position)
        {
        }
    }
}
