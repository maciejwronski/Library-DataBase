using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Library_Database
{
    public class PeselValidator
    {
        public static bool IsValid(string pesel)
        {
            var regex = new System.Text.RegularExpressions.Regex("^\\d{11}$");

            if (!regex.IsMatch(pesel))
            {
                return false;
            }

            int checkSum = PeselCheckSumCalculator.Calculate(pesel);
            int lastDigit = pesel.Last() - '0';

            return lastDigit == checkSum;
        }
    }
}
