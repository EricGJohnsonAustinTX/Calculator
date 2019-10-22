using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.CalculatorParser;

namespace Calculator.Factories
{
    public enum CalculationTypes
    {
        Max2Items,
        AnyNumberItems
    }
    public class CalculatorFactory
    {  
        public ICalcParser GetParser(CalculationTypes parserType)
        {
            switch (parserType)
            {
                case CalculationTypes.Max2Items:
                    return new Calculator2Items();
                case CalculationTypes.AnyNumberItems:
                    return new CalculatorManyItems();
                default:
                    return new CalculatorNull();
            }
        }
       
    }
}
