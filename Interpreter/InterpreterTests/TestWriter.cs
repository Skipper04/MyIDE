using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Interfaces;

namespace InterpreterTests
{
    class TestWriter : IWriter
    {
        public void WriteLine(string s)
        {
            throw new NotImplementedException();
        }

        public void Write(string outputString)
        {
            throw new NotImplementedException();
        }
    }
}
