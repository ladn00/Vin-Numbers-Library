using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarNumberLibrary
{
    public class CarNumber
    {
        int[] regions = { 77, 99, 97, 177, 199, 197, 777, 799, 797, 50, 90, 150, 190, 750, 790, 24, 84, 88, 124, 78, 98, 178, 198, 23, 93, 123, 193, 59, 81, 159, 38, 85, 138, 63, 163, 763, 66, 96, 196, 16, 116, 716, 02, 102, 702, 74, 174, 774 };
        char[] symbols = { 'А', 'В', 'Е', 'К', 'М', 'Н', 'О', 'Р', 'С', 'Т', 'У', 'Х' };

        /// <summary>
        /// Метод проверяет правильность введенного автомобильного номера
        /// </summary>
        public bool NumberCheck(string number)
        {
            bool isFine = true;

            string numberRegion = number.Length == 9 ? number.Substring(6, 3) : number.Substring(6, 2);
            if (!regions.Contains(Int32.Parse(numberRegion)))
                isFine = false;

            for (int i = 0; i < number.Length; i++)
            {
                if (i > 0 && i < 4)
                {
                    if (!Char.IsDigit(number[i]))
                        isFine = false;
                }
                if (i == 0 || (i > 3 && i < 6))
                {
                    if (!symbols.Contains(number[i]))
                        isFine = false;
                }
            }

            return isFine;
        }

        /// <summary>
        /// Метод формирует следующий за введенным номером
        /// </summary>
        public string NumberGenerate(string number)
        {
            string newNumber = number;
            string digitsSub = number.Substring(1, 3);
            if (digitsSub != "999")
            {
                newNumber = String.Concat(number[0], (Convert.ToInt32(digitsSub) + 1).ToString(),
                    number.Substring(3, number.Length - 4));
            }
            else if (number[5] != 'Х')
            {
                newNumber = String.Concat(number.Substring(0, 5), symbols[Array.IndexOf(symbols, number[5]) + 1],
                    number.Substring(6, number.Length - 6));
            }
            else
            {

                if (number[4] != 'Х')
                {
                    newNumber = String.Concat(number.Substring(0, 4), symbols[Array.IndexOf(symbols, number[4]) + 1],
                        number.Substring(5, number.Length - 5));
                }
                else if (number[0] != 'Х')
                {
                    newNumber = String.Concat(number.Substring(0, 1), symbols[Array.IndexOf(symbols, number[0]) + 1],
                        number.Substring(1, number.Length - 1));
                }
                else
                {
                    newNumber = newNumber.Replace('Х', 'А');
                }
            }
            return newNumber;
        }

        /// <summary>
        /// Метод формирует номер из определенного диапозона
        /// </summary>
        public string GenerateInRange(string firstNumber, string secondNumber)
        {
            // 0 4 5
            // 1 2 3
            var ran = new Random();
            string newNumber = "";
            string regionSub = firstNumber.Substring(6, 3);
            int ranChar, ranDigit;
            string digits = "";
            string symbolsString = "";

            for (int i = 0; newNumber.Length < 6; i++)
            {
                for (int j = 0; symbolsString.Length < 3; j++)
                {
                    ranChar = ran.Next(0, symbols.Length);
                    if (ranChar >= Array.IndexOf(symbols, firstNumber.Substring(0,3)[j]) && ranChar <= Array.IndexOf(symbols, secondNumber.Substring(0, 3)[j]))
                        symbolsString += symbols[ranChar];
                }

                for (int j = 0; digits.Length < 3; j++)
                {
                    ranDigit = ran.Next(0, 10);

                    if ((symbolsString[0] != firstNumber[0] || (symbolsString[0] == firstNumber[0] && firstNumber[j+1] <= ranDigit))
                        && (symbolsString[1] != firstNumber[4] || (symbolsString[1] == firstNumber[4] && firstNumber[j + 1] <= ranDigit))
                        && (symbolsString[2] != firstNumber[5] || (symbolsString[2] == firstNumber[5] && firstNumber[j + 1] <= ranDigit)))
                        digits += symbols[ranDigit];

                }
            }
            newNumber = String.Concat(symbolsString[0], digits, symbolsString.Substring(0,2), regionSub);
            return newNumber;
        }

    }

}
