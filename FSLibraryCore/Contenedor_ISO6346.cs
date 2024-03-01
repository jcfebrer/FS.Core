using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSLibrary
{
    /// <summary>
    /// Validate if Container number passes the DIN EN ISO 6346 validation Formula
    /// </summary>
    /// <example>
    /// <code>if(Contenedor_ISO6346.Check("Containernumber")) Console.WriteLine("Number is OK");</code></example>
    public static class Contenedor_ISO6346
    {
        //Alphabed and the parent Number from the DIN EN ISO 6346 Standard
        private static Dictionary<char, int> _Alphabet;

        /// <summary>
        /// ISO Validation Alphabed in a dictionary
        /// </summary>
        private static Dictionary<char, int> Alphabet
        {
            get
            {
                if (_Alphabet == null)
                {
                    //If _Alphabed is null Initialise new dictionary and fill it
                    _Alphabet = new Dictionary<char, int>();

                    //Add Letters
                    _Alphabet.Add('A', 10);
                    _Alphabet.Add('B', 12);
                    _Alphabet.Add('C', 13);
                    _Alphabet.Add('D', 14);
                    _Alphabet.Add('E', 15);
                    _Alphabet.Add('F', 16);
                    _Alphabet.Add('G', 17);
                    _Alphabet.Add('H', 18);
                    _Alphabet.Add('I', 19);
                    _Alphabet.Add('J', 20);
                    _Alphabet.Add('K', 21);
                    _Alphabet.Add('L', 23);
                    _Alphabet.Add('M', 24);
                    _Alphabet.Add('N', 25);
                    _Alphabet.Add('O', 26);
                    _Alphabet.Add('P', 27);
                    _Alphabet.Add('Q', 28);
                    _Alphabet.Add('R', 29);
                    _Alphabet.Add('S', 30);
                    _Alphabet.Add('T', 31);
                    _Alphabet.Add('U', 32);
                    _Alphabet.Add('V', 34);
                    _Alphabet.Add('W', 35);
                    _Alphabet.Add('X', 36);
                    _Alphabet.Add('Y', 37);
                    _Alphabet.Add('Z', 38);

                    //Add Numbers
                    _Alphabet.Add('0', 0);
                    _Alphabet.Add('1', 1);
                    _Alphabet.Add('2', 2);
                    _Alphabet.Add('3', 3);
                    _Alphabet.Add('4', 4);
                    _Alphabet.Add('5', 5);
                    _Alphabet.Add('6', 6);
                    _Alphabet.Add('7', 7);
                    _Alphabet.Add('8', 8);
                    _Alphabet.Add('9', 9);
                }

                return _Alphabet;
            }
        }

        /// <summary>
        /// Check if a Container number string is valid or not
        /// </summary>
        /// <param name="containerNumberToCheck">Container number string that has to be checked for validation</param>
        /// <returns>Boolean that shows if the Container number string is valid or not</returns>
        public static bool Check(string containerNumberToCheck)
        {
            //Clean the input string from Chars that are not in the Alphabed
            string containerNumber = CleanConNumberString(containerNumberToCheck);

            //Return true if the input string is empty
            //Used mostly for DataGridView to set the False validation only on false Container Numbers
            //and not empty ones
            if (containerNumber == string.Empty) return true;

            //Return False if the input string has not enough Characters
            if (containerNumber.Length != 11) return false;

            //Get the Sum of the ISO Formula
            double summ = GetSumm(containerNumber);

            //Calculate the Check number with the ISO Formula
            double tempCheckNumber = summ - (Math.Floor(summ / 11) * 11);

            //Set temCheckNumber 0 if it is 10 - In somme cases this is needed
            if (tempCheckNumber == 10) tempCheckNumber = 0;

            //Return true if the calculated check number matches with the input check number
            if (tempCheckNumber == GetCheckNumber(containerNumber))
                return true;

            //If no match return false
            return false;
        }

        /// <summary>
        /// Clean a Container number string from Chars that are not in the ISO Alphabed dictionary
        /// </summary>
        /// <param name="inputString">String that has to be cleaned</param>
        /// <returns>String that is cleaned from incorrect Chars</returns>
        private static string CleanConNumberString(string inputString)
        {
            //Set all Chars to Upper
            string resultString = inputString.ToUpper();

            //Loop Trough all chars
            foreach (char c in inputString)
            {
                //Remove Char if its not in the ISO Alphabet
                if (!Alphabet.Keys.Contains(c))
                    resultString = resultString.Replace(c.ToString(), string.Empty); //Remove chars with the String.Replace Method
            }

            //Return the cleaned String
            return resultString;
        }

        /// <summary>
        /// Provides the Check number from a Container number string
        /// </summary>
        /// <param name="inputString">String of the Container number</param>
        /// <returns>Integer of the Check number</returns>
        private static int GetCheckNumber(string inputString)
        {
            //Loop if string is longer than 1
            if (inputString.Length > 1)
            {
                //Get the last char of the string
                char checkChar = inputString[inputString.Length - 1];

                //Initialise a integer
                int CheckNumber = 0;

                //Parse the last char to a integer
                if (Int32.TryParse(checkChar.ToString(), out CheckNumber))
                    return CheckNumber; //Return the integer if the parsing can be done

            }

            //If parsing can´t be done and the string has just 1 char or is empty
            //Return 11 (A number that can´t be a check number!!!)
            return 11;
        }

        /// <summary>
        /// Calculate the sum by the ISO Formula
        /// </summary>
        /// <param name="inputString">String of the Container number</param>
        /// <returns></returns>
        private static double GetSumm(string inputString)
        {
            //Set summ to 0
            double summ = 0;

            //Calculate only if the container string is not empty
            if (inputString.Length > 1)
            {
                //Loop through all chars in the container string
                //EXCEPT the last char!!!
                for (int i = 0; i < inputString.Length - 1; i++)
                {
                    //Get the current char
                    char temChar = inputString[i];

                    //Initialise a integer to represent the char number in the ISO Alphabet
                    //Set it to 0
                    int charNumber = 0;

                    //If Char exists in the Table get it´s number
                    if (Alphabet.Keys.Contains(temChar))
                        charNumber = Alphabet[temChar];

                    //Add the char number to the sum using the ISO Formula
                    summ += charNumber * (Math.Pow(2, i));
                }
            }

            //Return the calculated summ
            return summ;
        }
    }
}
