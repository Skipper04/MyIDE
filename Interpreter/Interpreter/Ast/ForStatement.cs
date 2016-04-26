using System;

namespace Interpreter.Ast
{
    sealed class ForStatement : BranchStatement
    {
        public Condition Condition { get; private set; }
        public Statement Initializer { get; private set; }
        public Statement Iterator { get; private set; }
        public bool IsRun { get; set; }

        public ForStatement(Statement initializer, Condition condition, Statement iterator, 
            Statement body, Position position) : base(body, position)
        {
            if (initializer == null || condition == null || iterator == null || body == null)
            {
                throw new ArgumentNullException();
            }

            Initializer = initializer;
            Initializer.NextStatement = this;
            Condition = condition;
            Iterator = iterator;
            body.NextStatement = Iterator;
            Iterator.NextStatement = this;
        }
    }
}
