using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Values;

namespace Interpreter.Ast
{
    class Slice : Expression
    {
        public Expression Collection { get; private set; }
        public Expression Index { get; private set; }

        public Slice(Expression collection, Expression index, Position position)
            : base(position)
        {
            if (collection == null || index == null)
            {
                throw new ArgumentNullException();
            }

            Collection = collection;
            Index = index;
        }

        public override Value Calculate(Context context)
        {
            Value collectionValue = Collection.Calculate(context);
            Value indexValue = Index.Calculate(context);

            return collectionValue[indexValue];
        }
    }
}
