using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interpreter.Interfaces;

namespace MyIDE
{
    public class ResultWriter : IWriter
    {
        private readonly RichTextBox rtbOutput;

        public ResultWriter(RichTextBox rtbOutput)
        {
            if (rtbOutput == null)
            {
                throw new ArgumentNullException();
            }

            this.rtbOutput = rtbOutput;
        }

        public void Write(string outputString)
        {
            rtbOutput.Text += outputString;
        }
    }
}
