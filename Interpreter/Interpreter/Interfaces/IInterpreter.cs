using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Values;

namespace Interpreter.Interfaces
{
    public interface IInterpreter
    {
        void Build();
        void Run();
        List<IError> GetErrors();
        Dictionary<string, Value> GetVariables();
    }
}
