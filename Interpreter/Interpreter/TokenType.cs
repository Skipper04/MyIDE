namespace Interpreter
{
    public enum TokenType
    {
        Unknown,
        Number,
        String,
        Identifier,
        Plus,               //'+'
        Minus,              //'-'
        Multiply,           //'*'
        Divide,             //'/'
        Degree,             //'**'
        Assignment,         //'='
        Less,               //"<"
        LessOrEqual,        //"<="
        Equal,              //"=="
        NotEqual,           //"!="
        GreaterOrEqual,     //">="
        Greater,            //">"
        Negation,            //'!'
        OpenParenthesis,    //'('
        CloseParenthesis,   //')'
        OpenCurlyBrace,     //'{'
        CloseCurlyBrace,    //'}'
        OpenBracket,        //'['
        CloseBracket,       //']'
        Colon,              //':'
        If,                 //"if"
        Else,               //"else"
        For,                //"for"
        While,              //"while"
        Goto,               //"goto"
        Semicolon,          //';'
        Eof                 //end of file
    }
}
