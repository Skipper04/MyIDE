using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Interfaces;

namespace Interpreter
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }

        public void Write(string s)
        {
            Console.Write(s);
        }
    }
}
