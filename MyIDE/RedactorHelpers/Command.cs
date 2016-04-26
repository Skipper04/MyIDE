using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIDE.RedactorHelpers
{
    class Command
    {
        public readonly CommandType Type;
        public readonly int StartPosition;
        public readonly string ChangedText;

        public Command(CommandType type, int startPosition, string changedText)
        {
            Type = type;
            StartPosition = startPosition;
            ChangedText = changedText;
        }
    }
}
