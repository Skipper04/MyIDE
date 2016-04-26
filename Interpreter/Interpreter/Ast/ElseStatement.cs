using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Ast
{
    sealed class ElseStatement : Statement
    {
        private Statement nextStatement;
        public override Statement NextStatement
        {
            get { return nextStatement; }
            set
            {
                if (NextStatement != null)
                {
                    NextStatement.NextStatement = nextStatement;
                }
                else
                {
                    nextStatement = value;
                }
            }
        }

        public ElseStatement(Statement nextStatement, Position position) : base(position)
        {
            NextStatement = nextStatement;
        }
    }
}
