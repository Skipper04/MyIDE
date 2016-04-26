using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Ast
{
    public class Label : Node
    {
        public readonly string Name;

        public Label(string name, Position position) : base(position)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }

            if (name == string.Empty)
            {
                throw new ArgumentException("Name cann't be empty");
            }

            Name = name;
        }

        public bool Equals(Label other, StringComparison stringComparison)
        {
            return string.Equals(Name, other.Name, stringComparison);
        }

        public override int GetHashCode()
        {
            return Name == null ? 0 : Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Label other = obj as Label;
            return other != null && other.Name == this.Name;
        }
    }
}
