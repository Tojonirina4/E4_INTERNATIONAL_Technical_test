using NUnit.Framework;
using Test2StringCalculator.Services;
using Xunit;

namespace TestStringCalculator
{
    public class StringCalculator_Add
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnZeroForAGivenEmptyString()
        {
            var calculator = new StringCalculatorService();
            var result = calculator.Add(string.Empty);
            Assert.AreEqual(0, result);
        }
    }
}