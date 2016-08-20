using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using NUnit.Framework;
using Interpreter;
using Interpreter.Exceptions;
using NUnit.Framework.Constraints;

namespace InterpreterTests
{
    [TestFixture]
    public class LexerTests
    {
        private Lexer lexer;

        #region Parse number tests
        [Test]
        public void ParseIntegerNumberTest1()
        {
            const string number = "1";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number && 
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseIntegerNumberTest2()
        {
            const string number = "123";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseNegativeIntegerNumberTest()
        {
            const string number = "-123";
            lexer = new Lexer(number);
            lexer.GetNextTokenForParser();
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals("123", StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberTestWithoutSignificandTest1()
        {
            const string number = ".1";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberTestWithoutSignificandTest2()
        {
            const string number = ".123";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberExponentTest()
        {
            const string number = "1e3";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberPointTest1()
        {
            const string number = "1.3";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberPointTest2()
        {
            const string number = "1.23";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberPlusExponentPartTest1()
        {
            const string number = "1e+3";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberPlusExponentPartTest2()
        {
            const string number = "1e+32";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberMinusExponentPartTest()
        {
            const string number = "1e-2";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleExponentNumberTest()
        {
            const string number = "1e9";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberTest2()
        {
            const string number = "1.1e9";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberTest3()
        {
            const string number = "1.11e9";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberTest4()
        {
            const string number = "1.1e+9";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberTest5()
        {
            const string number = "1.1e-9";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals(number, StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberTest6()
        {
            const string number = "double a = 1.1e-9";
            int shift = 11;
            lexer = new Lexer(number);
            lexer.GetNextTokenForParser();
            lexer.GetNextTokenForParser();
            lexer.GetNextTokenForParser();
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals("1.1e-9", StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleNumberTest7()
        {
            const string number = "1)";
            Token token = new Lexer(number).GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals("1", StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleTest()
        {
            const string number = "1a.1";
            lexer = new Lexer(number);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.Number &&
                token.Name.Equals("1", StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDoubleErrorTest2()
        {
            const string number = "1.e1";
            lexer = new Lexer(number);
            try
            {
                Token token = lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest3()
        {
            const string number = "1ee1";
            lexer = new Lexer(number);
            try
            {
                lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest4()
        {
            const string number = "1e1e";
            lexer = new Lexer(number);
            try
            {
                lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest5()
        {
            const string number = ".e";
            lexer = new Lexer(number);
            try
            {
                lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest7()
        {
            const string number = "..e";
            lexer = new Lexer(number);
            try
            {
                Token token = lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest8()
        {
            const string number = "1.-e";
            lexer = new Lexer(number);
            try
            {
                Token token = lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest9()
        {
            const string number = "1.+e";
            lexer = new Lexer(number);
            try
            {
                Token token = lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest10()
        {
            const string number = "1.-1";
            lexer = new Lexer(number);
            try
            {
                Token token = lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest11()
        {
            const string number = "1.+";
            lexer = new Lexer(number);
            try
            {
                Token token = lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest12()
        {
            const string number = "1.";
            lexer = new Lexer(number);
            try
            {
                Token token = lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [Test]
        public void ParseDoubleErrorTest13()
        {
            const string number = ".";
            lexer = new Lexer(number);
            try
            {
                Token token = lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }


        #endregion 

        [Test]
        public void ParseStringLiteralTest1()
        {
            const string stringLiteral = "\"res\"";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.String &&
                token.Name.Equals("res", StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseStringLiteralTest2()
        {
            const string stringLiteral = "string s = \"res\";";
            lexer = new Lexer(stringLiteral);
            lexer.GetNextTokenForParser();
            lexer.GetNextTokenForParser();
            lexer.GetNextTokenForParser();
            Token token = lexer.GetNextTokenForParser();
            Assert.IsTrue(token.Type == TokenType.String &&
                token.Name.Equals("res", StringComparison.CurrentCulture));
        }

        [Test]
        public void ParseDegreeTokenTest()
        {
            const string stringLiteral = "**;";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
            Assert.AreEqual(TokenType.Degree, token.Type);
        }

        [Test]
        public void ParseEqualTokenTest()
        {
            const string stringLiteral = "==;";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
            Assert.AreEqual(TokenType.Equal, token.Type);
        }

        [Test]
        public void ParseGreaterOrEqualTokenTest()
        {
            const string stringLiteral = ">=;";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
            Assert.AreEqual(TokenType.GreaterOrEqual, token.Type);
        }

        [Test]
        public void ParseLessOrEqualTokenTest()
        {
            const string stringLiteral = "<=;";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
            Assert.AreEqual(TokenType.LessOrEqual, token.Type);
        }

        [Test]
        public void ParseNotEqualTokenTest()
        {
            const string stringLiteral = "!=;";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
            Assert.AreEqual(TokenType.NotEqual, token.Type);
        }

        [Test]
        public void ParseOpenBracketTest()
        {
            const string stringLiteral = "[";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
            Assert.AreEqual(TokenType.OpenBracket, token.Type);
        }

        [Test]
        public void ParseCloseBracketTest()
        {
            const string stringLiteral = "]";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
            Assert.AreEqual(TokenType.CloseBracket, token.Type);
        }

        [Test]
        public void ParseColonTest()
        {
            const string stringLiteral = "ss:;";
            lexer = new Lexer(stringLiteral);
            lexer.GetNextTokenForParser();
            Token token = lexer.GetNextTokenForParser();
            Assert.AreEqual(TokenType.Colon, token.Type);
        }

        [Test]
        public void IncorrectLexerTest()
        {
            const string stringLiteral = "аба";
            lexer = new Lexer(stringLiteral);
            try
            {
                lexer.GetNextTokenForParser();
            }
            catch (LexerException ex)
            {
                Assert.AreEqual(LexerException.ExceptionType.UnknownToken, ex.ExType);
            }
        }

        [ExpectedException(typeof(LexerException))]
        [Test]
        public void IcorrectStringLiteralTest1()
        {
            const string stringLiteral = "\"res";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
        }

        [ExpectedException(typeof(LexerException))]
        [Test]
        public void IcorrectStringLiteralTest2()
        {
            const string stringLiteral = @"""res
           
          
            ";
            lexer = new Lexer(stringLiteral);
            Token token = lexer.GetNextTokenForParser();
        }

        [Test]
        public void PositionsOneLineTest()
        {
            const string input = "int a = 5;";
            lexer = new Lexer(input);
            Assert.IsTrue(AreEqualPositions(GetTokensPosition(lexer, 5),
            new List<Position>
            {
                new Position(0, 0, 0, 3),
                new Position(0, 4, 4, 1),
                new Position(0, 6, 6, 1),
                new Position(0, 8, 8, 1),
                new Position(0, 9, 9, 1)
            }));
        }

        [Test]
        public void ReturnTokenTest()
        {
            const string input = "return a;";
            lexer = new Lexer(input);
            Assert.AreEqual(TokenType.Return, lexer.GetNextTokenForParser().Type);
        }

        [Test]
        public void PositionsMultilineTest()
        {
            const string input = @"double a = 5.5;
while (a > 5)
{
a = a - 1;
}";
            lexer = new Lexer(input);
            Assert.IsTrue(AreEqualPositions(GetTokensPosition(lexer, 19),
            new List<Position>
            {
                new Position(0, 0, 0, 6),
                new Position(0, 7, 7, 1),
                new Position(0, 9, 9, 1),
                new Position(0, 11, 11, 3),
                new Position(0, 14, 14, 1),
                new Position(1, 0, 17, 5), //while
                new Position(1, 6, 23, 1), 
                new Position(1, 7, 24, 1),
                new Position(1, 9, 26, 1), //>
                new Position(1, 11, 28, 1), //5
                new Position(1, 12, 29, 1), //)
                new Position(2, 0, 32, 1),
                new Position(3, 0, 35, 1), //a
                new Position(3, 2, 37, 1),
                new Position(3, 4, 39, 1),
                new Position(3, 6, 41, 1), //-
                new Position(3, 8, 43, 1),
                new Position(3, 9, 44, 1), //;
                new Position(4, 0, 47, 1),
            }));
        }

        private static List<Position> GetTokensPosition(Lexer lexer, int countTokensToGet)
        {
            List<Position> result = new List<Position>();

            for (int i = 0; i < countTokensToGet; i++)
            {
                result.Add(lexer.GetNextTokenForParser().Position);    
            }

            return result;
        }

        private static bool AreEqualPositions(List<Position> first, List<Position> second)
        {
            if (first.Count != second.Count)
            {
                return false;
            }

            for (int positionIndex = 0; positionIndex < first.Count; positionIndex++)
            {
                if (!first[positionIndex].Equals(second[positionIndex]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
