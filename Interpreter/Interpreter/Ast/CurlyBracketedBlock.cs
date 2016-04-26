namespace Interpreter.Nodes
{
    public class CurlyBracketedBlock : Node
    {
        private Block block;

        public CurlyBracketedBlock(Block block)
        {
            this.block = block;
        }

        //public override double Calculate(Context context)
        //{
        //    if (context == null)
        //    {
        //        throw new ArgumentNullException();
        //    }

        //    block.Calculate(context);
        //    return 0;
        //}
    }
}
