using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Interfaces;

namespace Interpreter
{
    public class FileReader : IReader
    {
        private readonly string path;

        public FileReader(string path)
        {
            this.path = path;
        }
        public string Read()
        {
            return File.Exists(path) ? File.ReadAllText(path) : null;
        }
    }
}
