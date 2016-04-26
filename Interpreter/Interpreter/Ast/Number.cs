using System;
using Interpreter.Values;
using Double = Interpreter.Values.Double;

namespace Interpreter.Ast
{
    public class Number : Expression
    {
        private readonly Value value;

        public Number(string value, Position position) : base(position)
        {
            value = value.Replace('.', ',');
            int intResult;
            double doubleResult;
            if (int.TryParse(value, out intResult))
            {
               this.value = new Int(intResult); 
            }
            else if (double.TryParse(value, out doubleResult))
            {
                this.value = new Double(doubleResult);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override Value Calculate(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            return value;
        }
    }
}
