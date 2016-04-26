using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public struct Token
    {
        public TokenType Type { get; private set; }
        public string Name { get; private set; }
        public Position Position { get; private set; }
        
        public Token(TokenType type, string name, Position position)
            : this()
        {
            if (name == null || position == null)
            {
                throw new ArgumentNullException();
            }

            Type = type;
            Name = name;
            Position = position;
        }

        public Token(TokenType type, Position position)
            : this(type, string.Empty, position)
        {
        }

        public static bool operator ==(Token first, Token second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Token first, Token second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Token))
                return false;

            Token token = (Token)obj;
            return Name == token.Name && Type == token.Type;
        }

        public Position GetNextPosition()
        {
            return new Position(Position.Line, Position.Column + Position.Length + 1, 
                Position.StartIndex + Position.Length + 1, 1);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
