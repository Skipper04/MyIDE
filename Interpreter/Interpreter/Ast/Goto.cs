using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Ast
{
    sealed class Goto : Statement
    {
        public Label Label { get; private set; }

        public Goto(Label label, Position position) : base(position)
        {
            if (label == null)
            {
                throw new ArgumentNullException();
            }

            Label = label;
        }
    }
}
