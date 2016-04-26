using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Exceptions;
using Interpreter.Interfaces;

namespace Interpreter
{
    public class Error : IError
    {
        private readonly Position position;
        private readonly string message;

        public Error(Position position, string message)
            : this()
        {
            if (message == null || position == null)
            {
                throw new ArgumentNullException();
            }

            this.position = position;
            this.message = message;
        }

        private Error()
        {
        }

        public Error(BaseException exception)
            : this(exception.Position, exception.GetMessage())
        {
        }

        public Error(ValueException exception, Position position)
            : this(position, exception.GetMessage())
        {
        }

        public int GetLine()
        {
            return position.Line;
        }

        public int GetColumn()
        {
            return position.Column;
        }

        public int GetInitialIndex()
        {
            return position.StartIndex;
        }

        public int GetLength()
        {
            return position.Length;
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
