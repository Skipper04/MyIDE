using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter;

namespace MyIDE.RedactorHelpers
{
    class Colorer
    {
        private Lexer lexer;
        private readonly List<Token> tokens = new List<Token>();
        private Token nextToken;
        public delegate void ColorizeDeligate(ColorizeEventArgs eventArgs);

        public event ColorizeDeligate Colorize;

        public void OnTextChanged(TextChangedEventArgs eventArgs)
        {
            int start = eventArgs.StartIndexAfterChanging;
            string text = eventArgs.TextAfterChanging;

            if (eventArgs.Type == TypeOfChange.UndoRedo &&
                eventArgs.StartIndexAfterChanging == eventArgs.StartIndexBeforeChanging &&
                eventArgs.LengthAfterChanging != eventArgs.LengthBeforeChanging)
            {
                start = 0;
            }

            if (eventArgs.Type == TypeOfChange.Added && !eventArgs.NextSymbol.Equals('\r'))
            {
                text = text.Insert(start, eventArgs.NextSymbol.ToString());
            }

            if (eventArgs.Type == TypeOfChange.BackSpaced)
            {
                if (eventArgs.StartIndexBeforeChanging <= 0 && eventArgs.LengthBeforeChanging <= 0)
                {
                    return;
                }
                
                int length = eventArgs.LengthBeforeChanging;
                
                if (length <= 0)
                {
                    length = 1;
                    start--;
                }

                text = text.Remove(start, length);
            }

            if (eventArgs.Type == TypeOfChange.Deleted)
            {
                if (eventArgs.StartIndexBeforeChanging == eventArgs.TextBeforeChanging.Length)
                {
                    return;
                }

                int length = eventArgs.LengthBeforeChanging;

                if (length <= 0)
                {
                    length = 1;
                }

                text = text.Remove(start, length);
            }

            if (string.IsNullOrEmpty(text))
            {
                if (Colorize != null)
                {
                    Colorize(new ColorizeEventArgs(new List<ColoredToken>(), eventArgs.Type));
                }

                return;
            }

            if (eventArgs.Type == TypeOfChange.DragAndDrop)
            {
                int before = eventArgs.StartIndexBeforeChanging;
                if (eventArgs.StartIndexBeforeChanging > eventArgs.StartIndexAfterChanging)
                {
                    before += eventArgs.LengthBeforeChanging;
                }

                tokens.Clear();
                GetTokens(before, before, eventArgs.TextAfterChanging);
                GetTokens(eventArgs.StartIndexAfterChanging, eventArgs.StartIndexAfterChanging + eventArgs.LengthAfterChanging, eventArgs.TextAfterChanging);
            }
            else
            {
                tokens.Clear();
                GetTokens(start, eventArgs.StartIndexBeforeChanging, text);
            }

            List<ColoredToken> coloredTokens = tokens.Select(token => new ColoredToken(token)).ToList();

            if (Colorize != null)
            {
                Colorize(new ColorizeEventArgs(coloredTokens, eventArgs.Type));
            }
        }

        private void GetTokens(int first, int second, string text)
        {
            int startOfInsert = Math.Min(first, second);
            int endOfInsert = Math.Max(first, second);

            int startPos = GetStartIndex(text, startOfInsert);
            int endPos = GetEndIndex(text, endOfInsert);

            lexer = new Lexer(text, startPos);
            nextToken = lexer.GetNextTokenForColorer(endPos);

            while (nextToken.Type != TokenType.Eof)
            {
                tokens.Add(nextToken);
                nextToken = lexer.GetNextTokenForColorer(endPos);
            }
        }

        private static int GetEndIndex(string input, int index)
        {
            if (index < 0)
            {
                index = 0;
            }

            if (index >= input.Length)
            {
                return input.Length - 1;
            }

            index++;

            while (index < input.Length - 1 && !input[index].Equals('\n'))
            {
                index++;
            }

            return index;
        }

        private static int GetStartIndex(string input, int index)
        {
            if (index >= input.Length)
            {
                index = input.Length;
            }
            if (index <= 0)
            {
                return 0;
            }

            do
            {
                index--;
            } while (index > 0 && !input[index].Equals('\n'));

            return index;
        }
    }
}
