using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.CalculatorParser
{
    public class ParserBase
    {     

        public bool TestForNullEmptyInput(string inputString, ref string outString)
        {
            if (inputString == string.Empty || inputString == null)
            {
                outString = "0 = 0";
                return true;
            }
            return false;
        }
        public string[] CommaParse(string inData)
        {
            string[] arrayData = inData.Split(',');
            return arrayData;
        }

        public int[] FixNonNumbers(string[] inData)
        {
            int[] outData = new int[inData.Length];
            for (int iterator = 0; iterator < inData.Length; iterator++)
            {
                if (int.TryParse(inData[iterator], out int intValue))
                    outData[iterator] = intValue;
                else
                    outData[iterator] = 0;
            }
            return outData;
        }

        public string ArrayToDisplayString(int[] inData, out int total)
        {
            string resultString = string.Empty;
            total = 0;
            foreach (int dataPoint in inData)
            {
                if (resultString.Length == 0)
                    resultString = dataPoint.ToString();
                else
                    resultString += ((dataPoint >= 0) ? "+" + dataPoint.ToString() : "-" + dataPoint.ToString());
                total += dataPoint;
            }

            resultString = resultString + "=" + total.ToString();
            return resultString;
        }
    }
}
