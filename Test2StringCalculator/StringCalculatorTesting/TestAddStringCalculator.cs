using Xunit;
using Test2StringCalculator.Services;
namespace StringCalculatorTesting
{
    public class Tests_String_CalculatorService
    {

        [Fact]
        public void Add_EmptyString_ReturnsZero()
        {
            var calculator = new StringCalculatorService();
            var result = calculator.Add(string.Empty);
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        public void Add_OneString_ReturnsSum(string numbers, int expectedResult)
        {
            var calculator = new StringCalculatorService();
            var result = calculator.Add(numbers);
            Assert.Equal(expectedResult, result);
        }
    }
}