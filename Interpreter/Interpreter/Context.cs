using System.Collections.Generic;
using Interpreter.Interfaces;
using Interpreter.Values;

namespace Interpreter
{
    public class Context
    {
        public readonly Dictionary<string, Value> VariableValues = new Dictionary<string, Value>();
        public List<IError> Errors = new List<IError>();
    }
}
