using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Factories;
using Calculator.CalculatorParser;

namespace UnitTestCalculator
{
    [TestClass]
    public class UnitTestMultipleItems
    {
        //Class Factory
        private readonly CalculatorFactory _calculator = new CalculatorFactory();

        [TestMethod]
        public void NullInput()
        {
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.AnyNumberItems);
            int result = parser.Add(null, ref display);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestManyNumbers()
        {

            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.AnyNumberItems);

            int result = parser.Add("5, 1, 33, 98", ref display);
            Assert.AreEqual(137, result);
        }

        [TestMethod]
        public void TestManyNumbersNegative()
        {

            int result = 0;
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.AnyNumberItems);
            try
            {
                result = parser.Add("5,-3, 1, 33, 98", ref display);
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e.InnerException);

                if (e.InnerException != null && (e.InnerException.Message == "NegativeValue"))
                {
                    Assert.AreEqual("-3", e.Message);
                }

            }
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestManyNumbersNewLine()
        {
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.AnyNumberItems);

            int result = parser.Add(" 33\n\nasf,55", ref display);
            Assert.AreEqual(88, result);
        }

        [TestMethod]
        public void TestManyNumbersCustomDelim()
        {
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.AnyNumberItems);

            int result = parser.Add("//Q\n,55Q123", ref display);
            Assert.AreEqual(178, result);
        }

        [TestMethod]
        public void TestManyNumbersCustomDelimMixed()
        {
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.AnyNumberItems);

            int result = parser.Add("//R\n//[QQQ]\n55R123,22QQQ25$$$124,66//[$$$]\n", ref display);
            Assert.AreEqual(415, result);
        }
        [TestMethod]
        public void SockItToIT()
        {
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.AnyNumberItems);

            int result = parser.Add("//R\n//[QQQ][$$$]\n55R123,22QQQ25,@@$$$124,66\n441", ref display);
            Assert.AreEqual(856, result);
        }
    }
}
