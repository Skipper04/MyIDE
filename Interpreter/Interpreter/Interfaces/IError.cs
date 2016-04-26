using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interfaces
{
    public interface IError 
    {
        int GetLine();
        int GetColumn();
        int GetInitialIndex();
        int GetLength();
        string GetMessage();
    }
}
