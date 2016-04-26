using System;

namespace MyIDE.RedactorHelpers
{
    public class UndoRedoEventArgs : EventArgs
    {
        public readonly int StartIndex;
        public readonly int Length;
        public readonly string Text;

        public UndoRedoEventArgs()
        {
        }

        public UndoRedoEventArgs(int startIndex, int length, string text)
        {
            StartIndex = startIndex;
            Length = length;
            Text = text;
        }
    }
}
