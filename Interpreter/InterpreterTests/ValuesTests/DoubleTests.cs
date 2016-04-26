using System;
using System.Collections.Generic;
using Interpreter.Values;
using NUnit.Framework;
using Double = Interpreter.Values.Double;
using Int = Interpreter.Values.Int;
using ValueType = Interpreter.ValueType;


namespace InterpreterTests.ValuesTests
{
    [TestFixture]
    class DoubleTests
    {
        private Double doubleValue; 
        [Test]
        public void ConstructorTest1()
        {
            double value = 5.5;
            doubleValue = new Double(value);
            Assert.AreEqual(value, doubleValue.DoubleValue);
        }

        [Test]
        public void ConstructorTest2()
        {
            doubleValue = new Double();
            Assert.AreEqual(new KeyValuePair<bool, bool>(true, true),
                new KeyValuePair<bool, bool>(doubleValue.Type == ValueType.Double, doubleValue.DoubleValue.Equals(0)));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetNullArgumentTest()
        {
            doubleValue = new Double();
            doubleValue.Set(null);
        }

        [Test]
        public void SetIntTest()
        {
            doubleValue = new Double();
            doubleValue.Set(new Int(5));
            Assert.AreEqual(true, doubleValue.DoubleValue.Equals(5));
        }

        [Test]
        public void SetDoubleTest()
        {
            doubleValue = new Double();
            doubleValue.Set(new Double(5.5));
            Assert.AreEqual(true, doubleValue.DoubleValue.Equals(5.5));
        }

        [Test]
        public void AddIntTest()
        {
            doubleValue = new Double(4.5);
            Value result = doubleValue.Add(new Int(4));
            Assert.AreEqual(true, result.Type == ValueType.Double && ((Double)result).DoubleValue.Equals(8.5));
        }

        [Test]
        public void AddDoubleTest()
        {
            doubleValue = new Double(4.5);
            Value result = doubleValue.Add(new Double(4.6));
            Assert.AreEqual(true, result.Type == ValueType.Double && ((Double)result).DoubleValue.Equals(9.1));
        }

        [Test]
        public void UnaryMinusDoubleTest()
        {
            doubleValue = new Double(4.5);
            Value result = doubleValue.Substract();
            Assert.AreEqual(true, result.Type == ValueType.Double && ((Double)result).DoubleValue.Equals(-4.5));
        }

        [Test]
        public void UnaryPlusDoubleTest()
        {
            doubleValue = new Double(4.5);
            Value result = doubleValue.Add();
            Assert.AreEqual(true, result.Type == ValueType.Double && ((Double)result).DoubleValue.Equals(4.5));
        }
    }
}
