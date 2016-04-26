using System;

namespace Interpreter.Ast
{
    internal sealed class IfStatement : BranchStatement
    {
        private Statement nextStatement;
        public Condition Condition { get; private set; }

        public override Statement NextStatement
        {
            get { return nextStatement; }
            set
            {
                if (NextStatement is ElseStatement)
                {
                    NextStatement.NextStatement = value;
                }
                else
                {
                    nextStatement = value;
                }
                TrueStatement.NextStatement = value;
            }
        }

        public IfStatement(Condition condition, Statement body, Position position) : base(body, position)
        {
            if (condition == null || body == null)
            {
                throw new ArgumentNullException();
            }
            body.NextStatement = NextStatement;
            Condition = condition;
        }
    }
}
