using System;
using System.Collections.Generic;

namespace MyIDE.RedactorHelpers
{
    class UndoRedoManager
    {
        private readonly Stack<Command> commands = new Stack<Command>();
        private readonly Stack<Command> undoneCommands = new Stack<Command>();

        private bool isChainStarted;

        public delegate void UndoRedoDeligate(UndoRedoEventArgs eventArgs);

        public event UndoRedoDeligate UndoRedo;

        public void OnTextChanged(TextChangedEventArgs eventArgs)
        {
            switch (eventArgs.Type)
            {
                case TypeOfChange.UndoRedo:
                    {
                        isChainStarted = false;
                        return;
                    }
                case TypeOfChange.DragAndDrop:
                    {
                        isChainStarted = false;
                        undoneCommands.Clear();
                        string changed = eventArgs.TextBeforeChanging.Substring(eventArgs.StartIndexBeforeChanging, eventArgs.LengthBeforeChanging);
                        if (!string.IsNullOrEmpty(changed))
                        {
                            commands.Push(new Command(CommandType.Deleting, eventArgs.StartIndexBeforeChanging, changed));
                            commands.Push(new Command(CommandType.Adding, eventArgs.StartIndexAfterChanging, changed));
                        }
                        break;
                    }
                case TypeOfChange.Inserted:
                    {
                        isChainStarted = false;
                        undoneCommands.Clear();
                        string deleted = eventArgs.TextBeforeChanging.Substring(eventArgs.StartIndexBeforeChanging, eventArgs.LengthBeforeChanging);
                        
                        if (!string.IsNullOrEmpty(deleted))
                        {
                            commands.Push(new Command(CommandType.Deleting, eventArgs.StartIndexBeforeChanging, deleted));
                        }

                        string added = eventArgs.TextAfterChanging.Substring(eventArgs.StartIndexBeforeChanging,
                            eventArgs.StartIndexAfterChanging - eventArgs.StartIndexBeforeChanging);

                        if (!string.IsNullOrEmpty(added))
                        {
                            commands.Push(new Command(CommandType.Adding, eventArgs.StartIndexBeforeChanging, added));
                        }
                        
                        break;
                    }
                case TypeOfChange.Added:
                    {
                        undoneCommands.Clear();
                        string deleted = eventArgs.TextBeforeChanging.Substring(eventArgs.StartIndexBeforeChanging,
                            eventArgs.LengthBeforeChanging);

                        if (!string.IsNullOrEmpty(deleted))
                        {
                            isChainStarted = false;
                            commands.Push(new Command(CommandType.Deleting, eventArgs.StartIndexBeforeChanging, deleted));
                        }

                        if (isChainStarted)
                        {
                            Command command = commands.Peek();
                            if (command.StartPosition + command.ChangedText.Length == eventArgs.StartIndexBeforeChanging &&
                                char.IsLetterOrDigit(eventArgs.NextSymbol))
                            {
                                commands.Pop();
                                commands.Push(new Command(CommandType.Adding, command.StartPosition,
                                    command.ChangedText + eventArgs.NextSymbol));
                                break;
                            }
                        }

                        isChainStarted = char.IsLetterOrDigit(eventArgs.NextSymbol);
                        commands.Push(new Command(CommandType.Adding, eventArgs.StartIndexBeforeChanging,
                                eventArgs.NextSymbol.ToString()));
                        break;
                    }
                case TypeOfChange.Deleted:
                    {
                        isChainStarted = false;
                        undoneCommands.Clear();
                        if (string.IsNullOrEmpty(eventArgs.TextBeforeChanging) ||
                            eventArgs.StartIndexBeforeChanging == eventArgs.TextBeforeChanging.Length)
                        {
                            return;
                        }

                        int start = eventArgs.StartIndexBeforeChanging;
                        int length = eventArgs.LengthBeforeChanging;

                        if (length <= 0)
                        {
                            length = 1;
                        }
                        string deleted = eventArgs.TextBeforeChanging.Substring(start, length);
                        commands.Push(new Command(CommandType.Deleting, start, deleted));
                        break;
                    }
                case TypeOfChange.BackSpaced:
                    {
                        isChainStarted = false;
                        undoneCommands.Clear();
                        if (string.IsNullOrEmpty(eventArgs.TextBeforeChanging) ||
                            (eventArgs.StartIndexBeforeChanging <= 0 && eventArgs.LengthBeforeChanging <= 0))
                        {
                            return;
                        }

                        int start = eventArgs.StartIndexBeforeChanging;
                        int length = eventArgs.LengthBeforeChanging;

                        if (length <= 0)
                        {
                            length = 1;
                            start--;
                        }

                        string deleted = eventArgs.TextBeforeChanging.Substring(start, length);
                        commands.Push(new Command(CommandType.Deleting, start, deleted));
                        break;
                    }
            }
        }

        public void OnUndo()
        {
            if (commands.Count <= 0)
            {
                return;
            }
            
            Command command = commands.Pop();
            UndoRedoEventArgs e = new UndoRedoEventArgs();
         
            switch (command.Type)
            {
                case CommandType.Deleting:
                    {
                        e = new UndoRedoEventArgs(command.StartPosition, 0, command.ChangedText);
                        break;
                    }
                case CommandType.Adding:
                    {
                        e = new UndoRedoEventArgs(command.StartPosition, command.ChangedText.Length, string.Empty);
                        break;
                    }
            }
            
            undoneCommands.Push(command);
            
            if (UndoRedo != null)
            {
                UndoRedo(e);
            }
        }

        public void OnRedo()
        {
            if (undoneCommands.Count <= 0)
            {
                return;
            }
            
            UndoRedoEventArgs e = new UndoRedoEventArgs();
            Command command = undoneCommands.Pop();
            
            switch (command.Type)
            {
                case CommandType.Adding:
                    {
                        e = new UndoRedoEventArgs(command.StartPosition, 0, command.ChangedText);
                        break;
                    }
                case CommandType.Deleting:
                    {
                        e = new UndoRedoEventArgs(command.StartPosition, command.ChangedText.Length, String.Empty);
                        break;
                    }
            }

            commands.Push(command);

            if (UndoRedo != null)
            {
                UndoRedo(e);
            }
        }
    }
}
