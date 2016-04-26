using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIDE.RedactorHelpers
{
    public class ColorizeEventArgs
    {
        public readonly List<ColoredToken> ColoredTokens;
        public readonly TypeOfChange Type;

        public ColorizeEventArgs(List<ColoredToken> coloredTokens, TypeOfChange type)
        {
            ColoredTokens = coloredTokens;
            Type = type;
        }
    }
}
