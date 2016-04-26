using System;

namespace Interpreter.Ast
{
    class WhileStatement : BranchStatement
    {
        public Condition Condition { get; private set; }

        public WhileStatement(Condition condition, Statement body, Position position) : base(body, position)
        {
            if (condition == null || body == null)
            {
                throw new ArgumentNullException();
            }

            body.NextStatement = this;
            Condition = condition;
        }
    }
}
