using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Factories;
using Calculator.CalculatorParser;

namespace UnitTestCalculator
{
    [TestClass]
    public class UnitTest2Items
    {        
        //Class Factory
        private readonly CalculatorFactory _calculator = new CalculatorFactory();

        [TestMethod]
        public void TwoGoodNumbers()
        {
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.Max2Items);
            int result = parser.Add("0,1", ref display);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TwoNumbersOneBad()
        {
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.Max2Items);
            int result = parser.Add("33,asf", ref display);
            Assert.AreEqual(33, result);
        }

        [TestMethod]
        public void NullInput()
        {
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.Max2Items);
            int result = parser.Add(null, ref display);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void AddSub3()
        {
            int result = 0;
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.Max2Items);
            try
            {
                result = parser.Add("0,1,3", ref display);
            }
            catch (Exception e)
            {
                if (e.Message != "Undefined Operation")
                    return;
            }
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestAddSubNeg()
        {
            string display = string.Empty;
            ICalcParser parser = _calculator.GetParser(CalculationTypes.Max2Items);

            int result = parser.Add("5,-3", ref display);
            Assert.AreEqual(2, result);
        }
    }
}
