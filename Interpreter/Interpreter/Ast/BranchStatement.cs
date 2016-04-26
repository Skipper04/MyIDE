using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Ast
{
    class BranchStatement : Statement
    {
        public virtual Statement TrueStatement { get; protected set; }

        public BranchStatement(Statement trueStatement, Position position) : base(position)
        {
            TrueStatement = trueStatement;
        }
    }
}
