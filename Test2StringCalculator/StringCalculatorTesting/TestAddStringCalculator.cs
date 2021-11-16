using Xunit;
using Test2StringCalculator.Services;
using System;

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

        [Theory]
        [InlineData("1, 1", 2)]
        [InlineData("2, 1", 3)]
        [InlineData("2, 2, 5", 9)]
        [InlineData("3, 2, 5", 10)]
        public void Add_MoreThanOneString_ReturnsSum(string numbers, int expectedResult)
        {
            var calculator = new StringCalculatorService();
            var result = calculator.Add(numbers);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1\n2, 3", 6)]
        [InlineData("1\n5\n3", 9)]
        public void Add_StringWithNewLine_ReturnsSum(string numbers, int expectedResult)
        {
            var calculator = new StringCalculatorService();
            var result = calculator.Add(numbers);
            Assert.Equal(expectedResult, result);
        }


        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//;\n1;2;7", 10)]
        [InlineData("//-\n1-3-7", 11)]
        [InlineData("//;\n1;2\n3,8", 14)]
        public void Add_StringWithCustomDelimiter_ReturnsSum(string numbers, int expectedResult)
        {
            var calculator = new StringCalculatorService();
            var result = calculator.Add(numbers);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("2, -1,3", "negatives not allowed: -1")]
        [InlineData("2, -1,-3", "negatives not allowed: -1,-3")]
        public void Add_StringWithNegativeNumbers_ThrowErrorWithNegativeNumbers(string numbers, string expectedResult)
        {
            var calculator = new StringCalculatorService();
            Action action = () =>  calculator.Add(numbers);
            var result = Assert.Throws<Exception>(action);
            Assert.Equal(expectedResult, result.Message);
        }
    }
}