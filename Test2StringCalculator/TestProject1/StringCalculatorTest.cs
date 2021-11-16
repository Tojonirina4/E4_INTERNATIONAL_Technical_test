using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Test2StringCalculator.Services;
namespace TestProject1
{
    [TestClass]
    public class StringCalculatorTest
    {
        [Fact]
        [TestMethod]
        public void AddEmptyString_ReturnsZero()
        {
            var calculator = new StringCalculatorService();
            var result = calculator.Add(string.Empty);
            Assert.AreEqual(0, result);

        }

        [Theory]
        [InlineData("1", 1)]
        [DataTestMethod]
        public void AddString_ReturnsSum(string numbers, int expectedResult)
        {
            var calculator = new StringCalculatorService();
            var result = calculator.Add(numbers);
            Assert.AreEqual(expectedResult, result);

        }
    }
}
