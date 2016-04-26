using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Ast
{
    public sealed class Block : Statement
    {
        private Statement nextStatement;
        public override Statement NextStatement {
            get
            {
                return nextStatement;
            }
            set
            {
                if (statements.Count > 0)
                {
                    nextStatement = statements.First();
                    statements.Last().NextStatement = value;        
                }
                else
                {
                    nextStatement = value;
                }
            }
        }
        private readonly List<Statement> statements;

        public Block(List<Statement> statements, Position position) : base(position)
        {
            if (statements == null || position == null)
            {
                throw new ArgumentNullException();
            }

            this.statements = statements;
            for (int i = 0; i < statements.Count - 1; i++)
            {
                statements[i].NextStatement = statements[i + 1];
            }
        }

        public Statement GetFirstStatement()
        {
            return statements.Count > 0 ? statements.First() : nextStatement;
        }
    }
}
