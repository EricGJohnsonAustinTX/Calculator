using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.CalculatorParser
{
    public interface ICalcParser
    {
        int Add(string data, ref string equation);
    }
}
