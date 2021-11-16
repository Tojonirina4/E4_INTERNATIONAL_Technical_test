using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test2StringCalculator.Services
{

    public interface IStringCalculatorService 
    {
        int Add(string numbers);
    }

    public class StringCalculatorService : IStringCalculatorService
    {
        public int Add(string _stringToCompute)
        {
            if (string.IsNullOrEmpty(_stringToCompute)) return 0;
            var delimiters = new List<Char> { ',', '\n'};
            var stringToCompute = Regex.Unescape(_stringToCompute);
            //Check if custom delimiters is submitted
            if (stringToCompute.StartsWith("//"))
            {
                //Take the customer delimiters and Append to delimiters
                var customerDelimiter = stringToCompute.Substring(2).ToCharArray()[0];
                delimiters.Add(customerDelimiter);
                stringToCompute = String.Join(customerDelimiter, stringToCompute.Split("\n").Skip(1).ToArray());
            }
            var splitedNumbers = stringToCompute.Split(delimiters.ToArray());
            //check negative numbers
            var negativeNumbers = splitedNumbers.Where(x => int.Parse(x) < 0);
            if (negativeNumbers.Count() > 0)
            {
                throw new Exception($"negatives not allowed:{String.Join(',', negativeNumbers.Select(x => x.ToString()))}");
            }
            var result = splitedNumbers.Select(x => int.Parse(x)).Sum();
            return result;
        }
    }
}
