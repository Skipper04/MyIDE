using System.Drawing;
using System.Linq;
using Interpreter;

namespace MyIDE.RedactorHelpers
{
    public class ColoredToken
    {
        public Color Color { get; private set; }
        public int StartIndex { get; private set; }
        public int Length { get; private set; }

        private static readonly string[] types =
        {
            "int", "double", "string"
        };

        private static readonly string[] lightSkyBlueIdentifiers =
        {
            "Print",
            "new"
        };
        
        public ColoredToken(Token token)
        {
            StartIndex = token.Position.StartIndex;
            Length = token.Position.Length;
            Color = GetColor(token);
        }

        private static Color GetColor(Token token)
        {
            switch (token.Type)
            {
                case TokenType.For:
                case TokenType.Else:
                case TokenType.Goto:
                case TokenType.If:
                case TokenType.While:
                    return Color.Blue;
                case TokenType.String:
                    return Color.Brown;
                case TokenType.Identifier:
                    string name = token.Name;
                    if (types.Contains(name))
                    {
                        return Color.Blue;
                    }
                    return lightSkyBlueIdentifiers.Contains(name) ? Color.LightSkyBlue : Color.Black;
                default:
                    return Color.Black;
            }
        }
    }
}
