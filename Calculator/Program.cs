using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.CalculatorParser;
using Calculator.Factories;

namespace Calculator
{
    class Program
    {
        //args
        //Array of 2 strings
        // Index 0 : bool - require 2 items
        //          true - Only allow 2
        //          false - allow many items
        // Index 1 : string of items
        //          string wrapped by ""
        //          Example: ConsoleApp1 true ""33,33,55,666, 1000,10025""
        static int Main(string[] args)
        {
            CalculatorFactory calc = new CalculatorFactory();

            string display = string.Empty;

            ICalcParser parser;

            if (bool.Parse(args[0]) == true)
                parser = calc.GetParser(CalculationTypes.Max2Items);
            else
                parser = calc.GetParser(CalculationTypes.AnyNumberItems);

            int sum = parser.Add(args[1], ref display);

            Console.WriteLine("Total is: " + sum.ToString());
            Console.WriteLine(display);
            return sum;

        }
    }
}
