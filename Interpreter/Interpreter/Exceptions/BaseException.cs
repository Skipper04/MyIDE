using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Exceptions
{
    public abstract class BaseException : Exception
    {
        public Position Position { get; private set; }
        
        protected BaseException(Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException();
            }
            Position = position;
        }

        public abstract string GetMessage();
    }
}
