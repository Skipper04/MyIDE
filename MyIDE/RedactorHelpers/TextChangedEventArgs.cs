namespace MyIDE.RedactorHelpers
{
    public class TextChangedEventArgs
    {
        public TypeOfChange Type { get; private set; }
        public int StartIndexAfterChanging { get; private set; }
        public int LengthAfterChanging { get; private set; }
        public string TextAfterChanging { get; private set; }
        public int StartIndexBeforeChanging { get; private set; }
        public string TextBeforeChanging { get; private set; }
        public int LengthBeforeChanging { get; private set; }
        public char NextSymbol { get; private set; }
        private const char defaultChar = ' ';

        public TextChangedEventArgs(TypeOfChange type, int startIndexAfterChanging, int lengthAfterChanging,
            string textAfterChanging, int startIndexBeforeChanging, int lengthBeforeChanging,
            string textBeforeChanging, char nextSymbol = defaultChar)
        {
            Type = type;
            StartIndexAfterChanging = startIndexAfterChanging;
            LengthAfterChanging = lengthAfterChanging;
            TextAfterChanging = textAfterChanging;
            StartIndexBeforeChanging = startIndexBeforeChanging;
            TextBeforeChanging = textBeforeChanging;
            LengthBeforeChanging = lengthBeforeChanging;
            NextSymbol = nextSymbol;
        }
    }
}
