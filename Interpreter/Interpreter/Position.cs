using System;

namespace Interpreter
{
    public class Position
    {
        public int Line { get; private set; }
        public int Column { get; private set; }
        public int StartIndex { get; private set; } //Start index in input program
        public int Length { get; private set; }

        public Position(int line, int column, int startIndex, int length)
            : this()
        {
            if (column < 0 || line < 0 || startIndex < 0 || length < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Column = column;
            Line = line;
            StartIndex = startIndex;
            Length = length;
        }

        public Position(int line, int column, int startIndex)
            : this(line, column, startIndex, 0)
        {
        }

        public Position()
        {
            Line = 0;
            Column = 0;
            StartIndex = 0;
            Length = 0;
        }

        public Position(Position start, Position end)
        {
            Line = start.Line;
            Column = start.Column;
            StartIndex = start.StartIndex;
            Length = end.StartIndex - start.StartIndex + end.Length;
        }

        public bool Equals(Position other)
        {
            return Line == other.Line && Column == other.Column &&
                   StartIndex == other.StartIndex && Length == other.Length;

        }

        public override string ToString()
        {
            return string.Format("Line {0} column {1}", Line, Column);
        }
    }
}
