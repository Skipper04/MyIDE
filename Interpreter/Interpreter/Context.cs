using System.Collections.Generic;
using Interpreter.Interfaces;
using Interpreter.Values;

namespace Interpreter
{
    public class Context
    {
        public readonly Dictionary<string, Value> VariableValues;
        public List<IError> Errors = new List<IError>();

        public Context(Dictionary<string, Value> variableValues)
        {
            VariableValues = variableValues;
        }

        public Context()
        {
            VariableValues = new Dictionary<string, Value>();
        }
    }
}
