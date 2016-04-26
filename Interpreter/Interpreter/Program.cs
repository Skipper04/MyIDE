using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Interpreter.Exceptions;
using Interpreter.Interfaces;
using Interpreter.Values;

namespace Interpreter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //const string stringLiteral = "**;";
            //Lexer lexer = new Lexer(stringLiteral);
            //Token token = lexer.GetNextTokenForParser();
            //Console.ReadKey();
            //Context context = new Context();
            //string programText = "int c = 0; int i = 1; for (; i < 5; i = i + 2) {c = i;}";
            //Parser parser = new Parser(programText);
            //Interpreter interpreter = new Interpreter(parser.Program(), context);
            //try
            //{
            //    interpreter.Interpret();
            //}
            //catch (InterpreterException ex)
            //{
            //    Console.WriteLine(ex.GetMessage());
            //    Console.WriteLine(ex.Position.ToString());
            //}
            //SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);

            //SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            //{
            //    {"a", new Int(5)},
            //    {"c", new Int(4)},
            //};
            //Console.WriteLine(AreEqual(variableValues, result));
            //Console.ReadKey();
            //return;
            //WorkWithUser();
        }

        private static bool AreEqual(SortedDictionary<string, Value> first, SortedDictionary<string, Value> second)
        {
            return Enumerable.SequenceEqual(first.Values, second.Values);
        }

        //private static void WorkWithUser()
        //{
        //    const string fileName = "input.txt";
        //    string path = Path.Combine(Environment.CurrentDirectory, fileName);
        //    IReader reader = new FileReader(path);
        //    IWriter writer = new ConsoleWriter();
        //    string programText = reader.Read();

        //    if (programText == null)
        //    {
        //        writer.WriteLine(string.Format("File {0} doesn't exist", path));
        //    }
        //    else
        //    {
        //        //try
        //        //{
        //        Ast.Program program = new Parser(programText).Program();
        //        Context context = new Context();
        //        Interpreter interpreter = new Interpreter(program, context);
        //        interpreter.Interpret();

        //        foreach (KeyValuePair<string, Value> values in context.VariableValues)
        //        {
        //            writer.WriteLine(string.Format("{0} = {1}", values.Key, values.Value));
        //        }
        //        Console.ReadKey();
        //        //}
        //        //catch (ParserException ex)
        //        //{
        //        //    switch (ex.ExType)
        //        //    {
        //        //        case ParserException.ExceptionType.IncorrectNameOfVariable:
        //        //            Console.WriteLine("Incorrect name of variable");
        //        //            break;
        //        //        case ParserException.ExceptionType.MissingAssignment:
        //        //            Console.WriteLine("Expected '='");
        //        //            break;
        //        //        case ParserException.ExceptionType.MissingCloseBlockBracket:
        //        //            Console.WriteLine("Expected '}'");
        //        //            break;
        //        //        case ParserException.ExceptionType.MissingCloseBracket:
        //        //            Console.WriteLine("Expected ')'");
        //        //            break;
        //        //        case ParserException.ExceptionType.MissingCompareOperator:
        //        //            Console.WriteLine("Expected compare operator");
        //        //            break;
        //        //        case ParserException.ExceptionType.MissingIf:
        //        //            Console.WriteLine("Expected \"If\"");
        //        //            break;
        //        //        case ParserException.ExceptionType.MissingOpenBlockBracket:
        //        //            Console.WriteLine("Expected '{'");
        //        //            break;
        //        //        case ParserException.ExceptionType.MissingOpenBracket:
        //        //            Console.WriteLine("Expected ')'");
        //        //            break;
        //        //        case ParserException.ExceptionType.MissingSemicolon:
        //        //            Console.WriteLine("Expected ';'");
        //        //            break;
        //        //        case ParserException.ExceptionType.IncorrectExpression:
        //        //            Console.WriteLine("Incorrect expression");
        //        //            break;
        //        //        default:
        //        //            throw new NotSupportedException();
        //        //    }
        //        //}
        //        //catch (LexerException ex)
        //        //{
        //        //    switch (ex.ExType)
        //        //    {
        //        //        case LexerException.ExceptionType.IncorrectSymbol:
        //        //            Console.WriteLine("Incorrect symbol");
        //        //            break;
        //        //    }
        //        //}
        //        //catch (InterpreterException ex)
        //        //{
        //        //    switch (ex.ExType)
        //        //    {
        //        //        case InterpreterException.ExceptionType.DivideByZero:
        //        //            Console.WriteLine("Divide by zero!!!");
        //        //            break;
        //        //        case InterpreterException.ExceptionType.NotDeclaredVariable:
        //        //            Console.WriteLine("Variable has not been declared");
        //        //            break;
        //        //        default:
        //        //            throw new NotSupportedException();
        //        //    }
        //        //}
        //    }
        //}
    }
}
