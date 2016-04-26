using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Values;
using String = Interpreter.Values.String;

namespace Interpreter.Ast
{
    class StringConstant : Expression
    {
        private readonly String value;

        public StringConstant(string value, Position position) : base(position)
        {
            this.value = new String(value);
        }

        public override Value Calculate(Context context)
        {
            return value;
        }
    }
}
