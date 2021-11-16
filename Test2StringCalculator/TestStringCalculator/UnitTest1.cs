using Test2StringCalculator.Services;
using Xunit;

namespace TestStringCalculator
{
    public class StringCalculator_Add_Tests
    {

        [Theory]
        [InlineData("0,0,0", 0)]
        [InlineData("0,1,2", 3)]
        [InlineData("1,2,3", 6)]
        public void Add_MultipleNumbers_ReturnsSumOfNumbers(string input, int expected)
        {
            var stringCalculator = new StringCalculatorService();

            var actual = stringCalculator.Add(input);

            Assert.Equal(expected, actual);
        }

    }
}