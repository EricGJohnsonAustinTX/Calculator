using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.CalculatorParser
{
    class CalculatorManyItems : ParserBase, ICalcParser
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

            //Fix Single char custom Delimiter
            data = ParseCustomSingleDelimiter(data);

            //Fix multi char and several Delimiters
            data = ParseMultipleCustomSingleDelimiter(data);

            //Fix New Lines
            data = ParseNewLine(data);

            //Convert to an array of int's as strings           
            string[] stringArray = CommaParse(data);

            //clean up non numbers            
            int[] intData = FixNonNumbers(stringArray);

            //Replace values greater then 1000 with 0
            intData = FixMaxValue(intData);

            string errorString = string.Empty;
            if (!ValidateNegativenumbers(intData, ref errorString))
            {
                throw new Exception(errorString, new Exception("NegativeValue"));
            }

            //Finish Operation           
            equation = ArrayToDisplayString(intData, out int total);

            return total;
        }

        public string ParseNewLine(string inData)
        {
            return inData.Replace("\n", ",");
        }

        public string ParseCustomSingleDelimiter(string inData)
        {
            int openIndex = 0;
            int closeIndex = 0;
            string delimiter = string.Empty;
            string remainsOfinData = inData;

            while (remainsOfinData.Length > 0)
            {
                openIndex = remainsOfinData.IndexOf("//", 0);
                closeIndex = remainsOfinData.IndexOf("\n", 0);
                if (openIndex == -1 || closeIndex == -1)
                    break;

                if (closeIndex - openIndex == 3)
                {
                    delimiter = remainsOfinData.Substring(remainsOfinData.IndexOf("//", 0) + 2, 1);
                    break;
                }
                else
                {
                    remainsOfinData = remainsOfinData.Substring(closeIndex + 2, remainsOfinData.Length - closeIndex - 2);
                }
            }

            if (delimiter == string.Empty)
                return inData;

            inData = inData.Replace("//" + delimiter + "\n", string.Empty);
            inData = inData.Replace(delimiter, ",");

            return inData;
        }

        public string ParseMultipleCustomSingleDelimiter(string inData)
        {
            int openIndex = 0;
            int closeIndex = 0;
            ArrayList delimiters = new ArrayList();
            string remainsOfinData = inData;

            openIndex = remainsOfinData.IndexOf("//[", 0);
            closeIndex = remainsOfinData.IndexOf("]\n", 0);
            if (openIndex == -1 || closeIndex == -1)
                return inData;

            while (remainsOfinData.Length > 0)
            {
                openIndex = remainsOfinData.IndexOf("//[", 0);
                closeIndex = remainsOfinData.IndexOf("]\n", 0);
                if (openIndex == -1 || closeIndex == -1)
                    break;

                string currentArray = remainsOfinData.Substring(remainsOfinData.IndexOf("//", 0) + 2, closeIndex - openIndex - 1);
                inData = inData.Replace("//" + currentArray + "\n", string.Empty);

                while (currentArray.Length > 0)
                {

                    openIndex = currentArray.IndexOf("[", 0);
                    closeIndex = currentArray.IndexOf("]", 0);
                    if (openIndex == -1 || closeIndex == -1)
                        break;

                    if (closeIndex - openIndex > 0)
                    {
                        delimiters.Add(currentArray.Substring(currentArray.IndexOf("//", 0) + 2, closeIndex - openIndex - 1));
                    }

                    currentArray = currentArray.Substring(closeIndex + 1, currentArray.Length - closeIndex - 1);
                }
                remainsOfinData = remainsOfinData.Substring(closeIndex + 4, remainsOfinData.Length - closeIndex - 4);
            }

            foreach (string delimiter in delimiters)
                inData = inData.Replace(delimiter, ",");

            return inData;
        }

        public bool ValidateNegativenumbers(int[] inData, ref string errorString)
        {
            bool hasError = true;
            foreach (int item in inData)
            {
                if (item < 0)
                {
                    if (errorString.Length == 0)
                        errorString = item.ToString();
                    else
                        errorString = "," + item.ToString();
                    hasError = false;
                }
            }
            return hasError;
        }

        public int[] FixMaxValue(int[] inData)
        {
            int[] outData = new int[inData.Length];
            for (int iterator = 0; iterator < inData.Length; iterator++)
            {
                if (inData[iterator] <= 1000)
                    outData[iterator] = inData[iterator];
                else
                    outData[iterator] = 0;
            }
            return outData;
        }
    }
}
