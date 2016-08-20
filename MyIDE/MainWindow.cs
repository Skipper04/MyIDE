using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Interpreter;
using Interpreter.Interfaces;
using MyIDE.RedactorHelpers;

namespace MyIDE
{
    public partial class MainWindow : Form
    {
        private bool isTextChanged;
        private const string caption = "My IDE";
        private const string saveMessage = "Would you like to save changes?";
        private const string filesFilter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        private const string homeDirectoryPath = "C:\\MyPrograms";
        private string fileName;
        private readonly IWriter writer;
        private IInterpreter interpreter;
        private char newChar;

        public delegate void TextChangedDeligate(TextChangedEventArgs args);
        public new event TextChangedDeligate TextChanged;
        public delegate void UndoRedoDeligate();
        public event UndoRedoDeligate Undo;
        public event UndoRedoDeligate Redo;

        private string textBeforeChanging;
        private int length;
        private int startPosition;

        public MainWindow()
        {
            InitializeComponent();
            Colorer colorer = new Colorer();
            UndoRedoManager undoRedoManager = new UndoRedoManager();
            TextChanged += colorer.OnTextChanged;
            TextChanged += undoRedoManager.OnTextChanged;
            colorer.Colorize += OnColorize;
            Undo += undoRedoManager.OnUndo;
            Redo += undoRedoManager.OnRedo;
            undoRedoManager.UndoRedo += OnUndoRedo;
            writer = new ResultWriter(rtbOutput);
        }

        private void OnOpenFileClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = filesFilter,
                InitialDirectory = homeDirectoryPath
            };


            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                Stream myStream = openFileDialog.OpenFile();
                using (myStream)
                {
                    StreamReader streamReader = new StreamReader(myStream);
                    char[] buffer = new char[myStream.Length];
                    streamReader.ReadBlock(buffer, 0, (int)myStream.Length);
                    rtbProgram.Text = new string(buffer);
                    TextChanged(new TextChangedEventArgs(TypeOfChange.Inserted, rtbProgram.TextLength, rtbProgram.TextLength,
                        rtbProgram.Text, 0, 0, string.Empty));
                    myStream.Close();
                    streamReader.Close();
                }

                fileName = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void OnFileNewMainMenuClick(object sender, EventArgs e)
        {
            CreateNewFile();
        }

        private void CreateNewFile()
        {
            rtbProgram.Text = string.Empty;
            isTextChanged = false;
            fileName = string.Empty;
        }

        private void TrySaveFile()
        {
            if (!isTextChanged)
            {
                return;
            }

            var dialogResult = MessageBox.Show(saveMessage, caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Yes)
            {
                SaveFile();
            }
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(fileName))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = filesFilter,
                    InitialDirectory = homeDirectoryPath
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (saveFileDialog.FileName == string.Empty)
                {
                    return;
                }

                fileName = saveFileDialog.FileName;
            }

            try
            {
                StreamWriter streamWriter = new StreamWriter(fileName);
                streamWriter.WriteLine(rtbProgram.Text);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void OnRtbProgramTextChanged(object sender, EventArgs e)
        {
            isTextChanged = true;
        }

        private void OnSaveMainMenuClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void OnExitMainMenuClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnBuildSolutionMainMenuClick(object sender, EventArgs e)
        {
            ClearData();
            interpreter = new Interpreter.Interpreter(rtbProgram.Text, writer);
            interpreter.Build();
            ShowData();
        }

        private void OnRunMainMenuClick(object sender, EventArgs e)
        {
            ClearData();
            interpreter = new Interpreter.Interpreter(rtbProgram.Text, writer);
            interpreter.Run();
            ShowData();
        }

        private void ShowData()
        {
            tcResults.SelectedTab = interpreter.GetErrors().Count == 0 ? tabOutput : tabErrors;
            ShowVariables();
            ShowOutput();
            ShowErrors();
        }

        private void ShowErrors()
        {
            List<IError> errors = interpreter.GetErrors();
            errors.Sort((first, second) => first.GetInitialIndex().CompareTo(second.GetInitialIndex()));

            foreach (var error in errors)
            {
                dgvErrors.Rows.Add(error.GetMessage(), error.GetLine() + 1, error.GetColumn() + 1,
                    error.GetInitialIndex(), error.GetLength());
            }
        }

        private void ShowVariables()
        {
            foreach (var variable in interpreter.GetVariables())
            {
                dgvVariables.Rows.Add(variable.Key, variable.Value.ToString());
            }
        }

        private void ShowOutput()
        {
        }

        private void ClearData()
        {
            rtbOutput.Clear();
            dgvErrors.Rows.Clear();
            dgvVariables.Rows.Clear();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            TrySaveFile();
        }

        private void dgvErrors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            rtbProgram.Focus();
            var selectedRows = dgvErrors.SelectedRows;
            for (int row = 0; row < selectedRows.Count; row++)
            {
                var cells = selectedRows[row].Cells;
                rtbProgram.Select(int.Parse(cells[columnInitialIndex.Name].Value.ToString()),
                int.Parse(cells[columnLength.Name].Value.ToString()));
            }
        }

        private void rtbProgram_KeyDown(object sender, KeyEventArgs e)
        {
            startPosition = rtbProgram.SelectionStart;
            length = rtbProgram.SelectionLength;
            textBeforeChanging = rtbProgram.Text;
            rtbProgram.ClearUndo();

            if (e.KeyData != (Keys.Control | Keys.V) && e.KeyData != (Keys.Control | Keys.C)
                && (e.Control || e.Alt || e.Shift))
            {
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Back && TextChanged != null)
            {
                TextChanged(new TextChangedEventArgs(TypeOfChange.BackSpaced, rtbProgram.SelectionStart,
                    rtbProgram.TextLength, rtbProgram.Text, startPosition, length, textBeforeChanging));
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Delete && TextChanged != null)
            {
                TextChanged(new TextChangedEventArgs(TypeOfChange.Deleted, rtbProgram.SelectionStart,
                    rtbProgram.TextLength, rtbProgram.Text, startPosition, length, textBeforeChanging));
                e.Handled = true;
            }

            switch (e.KeyData)
            {
                case Keys.Control | Keys.Z:
                    //if (Undo != null)
                    //{
                    //    Undo();
                    //}

                    if (TextChanged != null)
                    {
                        TextChanged(new TextChangedEventArgs(TypeOfChange.UndoRedo, rtbProgram.SelectionStart,
                            rtbProgram.TextLength, rtbProgram.Text, rtbProgram.SelectionLength + rtbProgram.SelectionStart,
                            length, textBeforeChanging));
                        rtbProgram.SelectionLength = 0;
                    }
                    return;
                case Keys.Control | Keys.Y:
                    if (Redo != null)
                    {
                        Redo();
                    }

                    if (TextChanged != null)
                    {
                        TextChanged(new TextChangedEventArgs(TypeOfChange.UndoRedo, rtbProgram.SelectionStart,
                            rtbProgram.TextLength, rtbProgram.Text, rtbProgram.SelectionLength + rtbProgram.SelectionStart,
                            length, textBeforeChanging));
                        rtbProgram.SelectionLength = 0;
                    }
                    break;
            }
        }

        private void rtbProgram_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsPunctuation(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) ||
                 char.IsSeparator(e.KeyChar) || e.KeyChar.Equals('\n') || e.KeyChar.Equals('\r')) && TextChanged != null)
            {
                newChar = e.KeyChar;
                TextChanged(new TextChangedEventArgs(TypeOfChange.Added, rtbProgram.SelectionStart, rtbProgram.TextLength,
                        rtbProgram.Text, startPosition, length, textBeforeChanging, e.KeyChar));
            }

            e.Handled = true;
        }

        private void rtbProgram_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V) && TextChanged != null)
            {
                TextChanged(new TextChangedEventArgs(TypeOfChange.Inserted, rtbProgram.SelectionStart, rtbProgram.TextLength,
                        rtbProgram.Text, startPosition, length, textBeforeChanging));
            }
        }

        public void OnColorize(ColorizeEventArgs args)
        {
            int start = rtbProgram.SelectionStart;

            if (args.Type == TypeOfChange.Added && !newChar.Equals('\r'))
            {
                rtbProgram.SelectedText = newChar.ToString();
                start++;
            }

            if (args.Type == TypeOfChange.BackSpaced)
            {
                if (length <= 0)
                {
                    length = 1;
                    start--;
                }

                rtbProgram.SelectionStart = start;
                rtbProgram.SelectionLength = length;
                rtbProgram.SelectedText = string.Empty;
            }

            if (args.Type == TypeOfChange.Deleted)
            {
                if (length <= 0)
                {
                    length = 1;
                }

                rtbProgram.SelectionStart = start;
                rtbProgram.SelectionLength = length;
                rtbProgram.SelectedText = string.Empty;
            }

            foreach (var token in args.ColoredTokens)
            {
                rtbProgram.SelectionStart = token.StartIndex;
                rtbProgram.SelectionLength = token.Length;
                rtbProgram.SelectionColor = token.Color;
            }

            rtbProgram.SelectionStart = start;
            rtbProgram.SelectionLength = 0;
        }

        public void OnUndoRedo(UndoRedoEventArgs eventArgs)
        {
            rtbProgram.SelectionStart = eventArgs.StartIndex;
            rtbProgram.SelectionLength = eventArgs.Length;
            rtbProgram.SelectedText = eventArgs.Text;
        }

    }
}
