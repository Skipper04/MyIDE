namespace Interpreter.Ast
{
    public class Program : Node
    {
        //private readonly Block block;
        private readonly Statement firstStatement;

        //public Statement GetFirstStatement()
        //{
        //    return block.GetFirstStatement();
        //}

        public Statement GetFirstStatement()
        {
            return firstStatement;
        }

        public Program(Statement firstStatement, Position position) : base(position)
        {
            this.firstStatement = firstStatement;
        }

        //public Program(Block block)
        //{
        //    if (block == null)
        //    {
        //        throw new ArgumentNullException();
        //    }

        //    this.block = block;
        //}
    }
}
