using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.CalculatorParser
{
    public class Calculator2Items : ParserBase, ICalcParser
    {
        //Add
        //returns Total 
        //Params:
        //string data - the string that holds list of numbers and delimiters
        //string ref equation - the string equivalent of the parsed summation
        public int Add(string data, ref string equation)
        {
            if (TestForNullEmptyInput(data, ref equation))
                return 0;

            //Convert to an array of int's as strings            
            string[] stringArray = CommaParse(data);

            if (stringArray.Length > 2)
                throw new System.Exception("Add Method Can only have 2 Items");

            //clean up non numbers             
            int[] intData = FixNonNumbers(stringArray);

            //Finish Operation            
            equation = ArrayToDisplayString(intData, out int total);
            return total;
        }
    }
}
