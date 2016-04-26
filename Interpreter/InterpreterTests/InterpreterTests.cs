using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Interpreter;
using Interpreter.Exceptions;
using Interpreter.Ast;
using Interpreter.Interfaces;
using Interpreter.Values;
using InterpreterObj = Interpreter.Interpreter;
using String = Interpreter.Values.String;
using Array = Interpreter.Values.Array;
using ValueType = Interpreter.ValueType;

namespace InterpreterTests
{
    [TestFixture]
    class InterpreterTests
    {
        private Context context;
        private string programText;
        private Parser parser;
        private InterpreterObj interpreter;
        private IWriter writer = new TestWriter();

        #region Double tests
        [Test]
        public void DoubleTest1()
        {
            context = new Context();
            programText = "double d = 5;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(5)}
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest2()
        {
            context = new Context();
            programText = "double d = 5; double c = 8; double a = d + c;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(5)},
                {"c", new Interpreter.Values.Double(8)},
                {"a", new Interpreter.Values.Double(13)}
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest3()
        {
            context = new Context();
            programText = @"double d = 5; d = 0;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(0.0)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest4()
        {
            context = new Context();
            programText = @"double d = 5; double c = 8; double a = d + c;double b = (a + d)*2; 
c = 4; a = c+b;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(5)},
                {"c", new Interpreter.Values.Double(4)},
                {"a", new Interpreter.Values.Double(40)},
                {"b", new Interpreter.Values.Double(36)}
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest5()
        {
            programText = "double d;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = 
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", ValueHelper.CreateValue(ValueType.Double)},
            };
            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest6()
        {
            programText = "double d; d = 5;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = 
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest7()
        {
            context = new Context();
            programText = "double d = 4/3;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest8()
        {
            context = new Context();
            programText = "double d = 45*0.2;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(9)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest9()
        {
            context = new Context();
            programText = "double d = ((5+6)*2+1)/3+45*0.2;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(16)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest10()
        {
            context = new Context();
            programText = "double d = 2**2;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(4)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleTest11()
        {
            context = new Context();
            programText = "double d = (0.2 + 0.4 - 0.001)**2.2;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(0.323846351994)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleUnaryMinusTest1()
        {
            programText = "double d = -1;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = 
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(-1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleUnaryMinusTest2()
        {
            programText = "double d = --1.0;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleUnaryPlusTest1()
        {
            programText = "double d = +1;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleUnaryPlusTest2()
        {
            programText = "double d = ++++++1;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleMixedUnaryOperationTest1()
        {
            programText = "double d = +--++-1;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(-1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void DoubleMixedUnaryOperationTest2()
        {
            programText = "double d = +--+---+-1;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }
        #endregion

        #region Goto tests
        [Test]
        public void GotoTest1()
        {
            programText = "goto f; int a = 5; f: int p = 4;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"p", new Int(4)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void GotoTest2()
        {
            programText = "goto f; f: int p = 4; int a = 5;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"p", new Int(4)},
                {"a", new Int(5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void gotoTest3()
        {
            programText = "goto f; int p = 4; f:int a = 5;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void TwoGotoTest()
        {
            programText = @"int k = 1; int p = 5; int a; goto f; g: p = a; f:a = p + 2; 
                            if (k == 1) {k = 0; goto g; }";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(9)},
                {"p", new Int(7)},
                {"k", new Int(0)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void GotoWithoutLabelTest()
        {
            programText = @"goto f;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            List<IError> errors = interpreter.GetErrors();
            Assert.IsTrue(IsOneParserError(errors, ParserException.ExceptionType.LableNotFound));
        }

        [Test]
        public void GotoTwoLabelsTest()
        {
            programText = @"goto f; f: int a = 5; f: int f = 5;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            List<IError> errors = interpreter.GetErrors();
            Assert.IsTrue(IsOneParserError(errors, ParserException.ExceptionType.LableDuplicate));
        }

        [Test]
        public void WithoutGotoTest1()
        {
            programText = @"f: int a = 5; f: int f = 5;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            List<IError> errors = interpreter.GetErrors();
            Assert.IsTrue(IsOneParserError(errors, ParserException.ExceptionType.LableDuplicate));
        }

        [Test]
        public void WithoutGotoTest2()
        {
            programText = @"f: int a = 5;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(5)},
            };
        }

        #endregion

        #region Int tests
        [Test]
        public void IntTest1()
        {
            context = new Context();
            programText = "int a = 5;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntTest2()
        {
            context = new Context();
            programText = "int a; a = 5;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntTest3()
        {
            context = new Context();
            programText = "int a = 5 + 7;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(12)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntTest4()
        {
            context = new Context();
            programText = "int a = 5 - 7;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(-2)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntTest5()
        {
            context = new Context();
            programText = "int a = 6 / 2;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(3)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntTest6()
        {
            programText = "int a = 6 / 2; int c = 2; double d = a / c;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = 
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(3)},
                {"c", new Int(2)},
                {"d", new Interpreter.Values.Double(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ConvertDoubleToIntErrorTest()
        {
            programText = "double d = 7.88; int a = d;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotConvert,
                ValueType.Double, ValueType.Int);
        }

        [Test]
        public void ConvertStringToIntErrorTest()
        {
            programText = "string s = \"a\"; int a = s;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotConvert,
                ValueType.String, ValueType.Int);
        }

        [Test]
        public void IntAddTest()
        {
            programText = "int a = 6; int b = 7; a = +a + b + a + b;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(26)},
                {"b", new Int(7)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntSubcstractTest()
        {
            programText = "int a = 6; int b = 7; a = -a + b + a - b;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(0)},
                {"b", new Int(7)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntMultiplyTest()
        {
            programText = "int a = 6; int b = 7; a = a * b * b;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(294)},
                {"b", new Int(7)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntMultiplyOverflowTest()
        {
            programText = "int a = 600000; int b = a; a = a * b * b * b;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(-2130706432)},
                {"b", new Int(600000)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntAssignBigNumberTest()
        {
            programText = "int a = 6000000000000000;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotConvert,
                ValueType.Double, ValueType.Int);
        }

        [Test]
        public void IntDivideTest1()
        {
            programText = "int a = 3; int b = 2; a = 3 / 2;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(1)},
                {"b", new Int(2)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntDivideTest2()
        {
            programText = "int a = 4; int b = 2; a = 4 / 2;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(2)},
                {"b", new Int(2)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IntDivideByZeroTest()
        {
            programText = "int a = 4 / 0;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            Assert.IsTrue(IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.DivideByZero));
        }

        [Test]
        public void IntDeclarateStringConstantTest()
        {
            programText = "int a = \"s\";";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            Assert.IsTrue(IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotConvert, 
                ValueType.String, ValueType.Int));
        }

        [Test]
        public void IntDeclarateStringTest()
        {
            programText = "string s = \"s\"; int a = s;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            Assert.IsTrue(IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotConvert,
                ValueType.String, ValueType.Int));
        }

        [Test]
        public void IntAssignStringConstantTest1()
        {
            programText = "int a; a = \"s\";";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            Assert.IsTrue(IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotConvert,
                ValueType.String, ValueType.Int));
        }

        [Test]
        public void IntAssignStringConstantTest2()
        {
            programText = "int a = 5; a = \"s\";";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            Assert.IsTrue(IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotConvert,
                ValueType.String, ValueType.Int));
        }


        [Test]
        public void IntAssignNumberConstantTest()
        {
            programText = "int a; a = 5;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(5)},
            };
        }

        [Test]
        public void IntAssignStringTest()
        {
            programText = "string s = \"s\"; int a = s;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            Assert.IsTrue(IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotConvert,
                ValueType.String, ValueType.Int));
        }
        #endregion

        #region String tests
        [Test]
        public void StringDeclarationTest()
        {
            programText = "string s;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"s", ValueHelper.CreateValue(ValueType.String)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void StringDeclarationEmptyStringTest()
        {
            programText = "string s = \"\";";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"s", new String(string.Empty)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void StringAssignmentTest()
        {
            programText = @"string s = ""dddd"";";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"s", new String("dddd")},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void AddStringsTest()
        {
            programText = @"string s = ""dddd""; string f = ""aaaa""; string sf = s + f; string fs = f + s;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"s", new String("dddd")},
                {"f", new String("aaaa")},
                {"fs", new String("aaaadddd")},
                {"sf", new String("ddddaaaa")},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void StringEqualsTest()
        {
            programText = @"string s = ""dddd""; string f = s; if (f == s) {f = """";}";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"s", new String("dddd")},
                {"f", new String(string.Empty)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void StringNotEqualsTest()
        {
            programText = @"string s = ""dddd""; string f = s + ""ss""; if (f != s) f = """";";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"s", new String("dddd")},
                {"f", new String(string.Empty)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void StringsNotEqualTypesConflictTest()
        {
            programText = @"string s = ""dddd""; double d = 5.6; if (s != d) s = """";";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            Assert.IsTrue(IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotCalculate));
        }

        [Test]
        public void StringsAddTypesConflictTest()
        {
            programText = @"string s = ""dddd"" + 5.6;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            Assert.IsTrue(IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotCalculate));
        }
        #endregion

        #region If tests
        [Test]
        public void IfTest1()
        {
            context = new Context();
            programText = "if (1 < 2) double d = 5;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfTest2()
        {
            context = new Context();
            programText = "if (1 < 2) {double d = 5; d = d + 0.5;}";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"d", new Interpreter.Values.Double(5.5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfTest3()
        {
            context = new Context();
            programText = "int a = 5; int b = 2; if (a > b) {int c = a; a = b; b = c;}";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(2)},
                {"b", new Int(5)},
                {"c", new Int(5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfTest4()
        {
            context = new Context();
            programText = "if (1 > 2) int c = 1; int c = 2;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"c", new Int(2)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfTest5()
        {
            context = new Context();
            programText = "int a = 5; int b = 2; if (a < b) {int c = a; a = b; b = c;} int c = a+b;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(5)},
                {"b", new Int(2)},
                {"c", new Int(7)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfTest6()
        {
            context = new Context();
            programText = "if (1 == 1) int c = 5;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"c", new Int(5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfElseTest1()
        {
            context = new Context();
            programText = "if (2 < 1) {int c = 5;} else int c = 1;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"c", new Int(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfElseTest2()
        {
            context = new Context();
            programText = "if (2 < 1) {int c = 5;} else {int c = 1;}";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"c", new Int(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfElseTest3()
        {
            context = new Context();
            programText = "if (2 < 1) {int c = 5;} else if (2 == 1) {int c = 1;}";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>();

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfElseTest4()
        {
            context = new Context();
            programText = "if (2 < 1) {int c = 5;} else if (2 == 1) {int c = 1;} else c = 10;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            List<IError> errors = interpreter.GetErrors();
            Assert.IsTrue(IsOneInterpreterError(errors, InterpreterException.ExceptionType.NotDeclaredVariable));
        }

        [Test]
        public void IfElseTest5()
        {
            context = new Context();
            programText = "if (2 < 1) {int c = 5;} else if (2 == 1) {int c = 1;} else int c = 10;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"c", new Int(10)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void IfElseTest6()
        {
            context = new Context();
            programText = "int a = 5; int b = 2; if (a < b) {int c = a; a = b; b = c;} else int c = a+b;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(5)},
                {"b", new Int(2)},
                {"c", new Int(7)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        #endregion

        #region While tests

        [Test]
        public void WhileTest1()
        {
            context = new Context();
            programText = "int a = 5;while(a > 0) a = a - 1;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(0)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void WhileTest2()
        {
            context = new Context();
            programText = "int a = 5; int c = 1; while(a > 0) {a = a - 1; c = c + 1;}";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(0)},
                {"c", new Int(6)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void WhileTest3()
        {
            context = new Context();
            programText = "int a = 5; int c = 1; while(a > 0) {a = a - 1; c = c + 1;} a = 1; c = 1;";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(1)},
                {"c", new Int(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void WhileTest4()
        {
            context = new Context();
            programText = "int a = 5; while(a >= 0) a = a - 1; ";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Int(-1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        #endregion

        #region For tests

        [Test]
        public void ForTest1()
        {
            context = new Context();
            programText = "int c = 0; for (int i = 0; i < 5; i = i + 1) {c = i;}";
            parser = new Parser(programText);
            interpreter = new InterpreterObj(parser.Program(), context);
            interpreter.Interpret();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(context.VariableValues);
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"i", new Int(5)},
                {"c", new Int(4)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ForTest2()
        {
            programText = @"int c = 0;
                            for (int a = 5; a > 0; a = a - 1)
                            {
                                c = c + 1;
                            }";
            interpreter = new Interpreter.Interpreter(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues =
                new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"c", new Int(5)},
                {"a", new Int(0)}
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ForWithoutInitilizerTest()
        {
            programText = "int c = 0; int i = 1; for (; i < 5; i = i + 2) {c = i;}";
            interpreter = new Interpreter.Interpreter(programText, writer);
            interpreter.Run();
            Assert.IsTrue(interpreter.GetErrors().Any());
        }

        [Test]
        public void ForWithoutConditionTest()
        {
            programText = "int c = 0; int i; for (i = 5; ; i = i + 2) {c = i;}";
            interpreter = new Interpreter.Interpreter(programText, writer);
            interpreter.Run();
            Assert.IsTrue(interpreter.GetErrors().Any());
        }

        [Test]
        public void ForWithoutIteratorTest()
        {
            programText = "int c = 0; int i; for (i = 5; i < 10;) {c = i;}";
            interpreter = new Interpreter.Interpreter(programText, writer);
            interpreter.Run();
            Assert.IsTrue(interpreter.GetErrors().Any());

        }

        [Test]
        public void ForIncorrectIteratorTest()
        {
            programText = "for (int i = 0; i < 5; i = \"s\"){}";
            interpreter = new Interpreter.Interpreter(programText, writer);
            interpreter.Run();
            Assert.IsTrue(IsOneValueError(interpreter.GetErrors(), ValueException.ExceptionType.CannotConvert,
                ValueType.String, ValueType.Int));

        }

        #endregion
        #region Array tests
        [Test]
        public void ArrayWithoutInitializationTest()
        {
            programText = "int[] a;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Array(ValueType.Int)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ArrayWithInitializationTest()
        {
            programText = "int[] a = new int[5];";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Array(ValueType.Int, 5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ArrayAssignmentTest1()
        {
            programText = "int[] a; a = new int[5];";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Array(ValueType.Int, 5)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ArrayAssignmentTest2()
        {
            programText = "int[] a = new int[5]; int[] b = new int[7]; a = b;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", new Array(ValueType.Int, 7)},
                {"b", new Array(ValueType.Int, 7)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ArrayAssignmentTest3()
        {
            programText = "int[] a = new int[2]; a[0] = 1; int b = a[0];";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", CreateArray(new Value[]{new Int(1), new Int(0)})},
                {"b", new Int(1)},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ArrayAssignmentTest4()
        {
            programText = "int[] a = new int[2]; a[0] = 1; int[] b = a; b[1] = b[0];";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", CreateArray(new Value[]{new Int(1), new Int(1)})},
                {"b", CreateArray(new Value[]{new Int(1), new Int(1)})},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ArrayAssignmentTest5()
        {
            programText = "int[] a = new int[2]; a[1] = 1; a[0] = a[1]; ";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", CreateArray(new Value[]{new Int(1), new Int(1)})},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ArrayAssignmentTest6()
        {
            programText = "int[] a = new int[2]; int[] b = a; ";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", CreateArray(new Value[]{new Int(0), new Int(0)})},
                {"b", CreateArray(new Value[]{new Int(0), new Int(0)})},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        [Test]
        public void ArrayElementAssignmentTest()
        { 
            programText = "int[] a; a = new int[2]; a[0] = 1;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            SortedDictionary<string, Value> variableValues = new SortedDictionary<string, Value>(interpreter.GetVariables());
            SortedDictionary<string, Value> result = new SortedDictionary<string, Value>
            {
                {"a", CreateArray(new Value[]{new Int(1), new Int(0)})},
            };

            Assert.IsTrue(AreEqual(result, variableValues));
        }

        #endregion
        [Test]
        public void Fibonacci10NumberTest()
        {
            programText = @"
            int fibNum = 10;
            int f1 = 1;
            int f2 = 1;
            int fibResult;
            if (fibNum < 1)
            {
                fibResult = -1;
            }
            if (fibNum == 1)
            {
                fibResult = f1;
            }
            else if (fibNum == 2)
            {
                fibResult = f2;
            }
            else
            {
                int temp;
                for (int i = 3; i <= fibNum; i = i + 1)
                {
                    f1 = f2 + f1;
                    temp = f1;
                    f1 = f2;
                    f2 = temp;
                }
                fibResult = f2;
            }";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            Assert.IsTrue(new Int(55).IsEqual(interpreter.GetVariables()["fibResult"]).BoolValue);
        }

        [Test]
        public void HasBeenDeclaredVariableTest()
        {
            programText = "double a = 6; double a;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            List<IError> errors = interpreter.GetErrors();
            Assert.IsTrue(IsOneInterpreterError(errors,
                InterpreterException.ExceptionType.AlreadyHasBeenDeclaredVariable));
        }

        [Test]
        public void NotDeclaredVariableTest()
        {
            programText = "a = 6;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            List<IError> errors = interpreter.GetErrors();
            Assert.IsTrue(IsOneInterpreterError(errors,
                InterpreterException.ExceptionType.NotDeclaredVariable));
        }

        [Test]
        public void VariableAlreadyDeclaredTest()
        {
            programText = "int a = 6; int a = 5;";
            interpreter = new InterpreterObj(programText, writer);
            interpreter.Run();
            var errors = interpreter.GetErrors();
            Assert.IsTrue(IsOneInterpreterError(errors,
                InterpreterException.ExceptionType.AlreadyHasBeenDeclaredVariable));
        }

        private static bool AreEqual(SortedDictionary<string, Value> first, SortedDictionary<string, Value> second)
        {
            return first.Values.SequenceEqual(second.Values) &&
                   first.Keys.SequenceEqual(second.Keys);
        }

        private bool IsOneInterpreterError(List<IError> errors, InterpreterException.ExceptionType exceptionType)
        {
            return errors.Count == 1 && errors[0].GetMessage().Equals(
                new InterpreterException(exceptionType, new Position()).GetMessage());
        }

        private bool IsOneValueError(List<IError> errors, ValueException.ExceptionType exceptionType,
            params Interpreter.ValueType[] types)
        {
            return errors.Count == 1 && errors[0].GetMessage().Equals(
                new ValueException(exceptionType, types).GetMessage());
        }

        private bool IsOneParserError(List<IError> errors, ParserException.ExceptionType exceptionType)
        {
            return errors.Count == 1 && errors[0].GetMessage().Equals(
                new ParserException(exceptionType, new Position()).GetMessage());
        }

        private Array CreateArray(Value[] values)
        {
            if (values.Length == 0)
            {
                throw new ArgumentException("Array cannot be empty");
            }

            Array result = new Array(values[0].Type, values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                result[new Int(i)].Set(values[i]);
            }

            return result;
        }
    }
}
