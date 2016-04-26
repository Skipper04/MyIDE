using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIDE.RedactorHelpers
{
    public enum TypeOfChange
    {
        Added,
        BackSpaced,
        Deleted,
        UndoRedo,
        Inserted,
        DragAndDrop
    }
}
