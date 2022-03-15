using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstInvalidFormatTests
    {
        [Theory]
        [InlineData("12345",@"\d{1,6}")]
        [InlineData("50FA", @"[0-9a-fA-F]{1,6}")]
        [InlineData("abfACD", @"[a-fA-F]{1,8}")]
        [InlineData("DHSTRY",@"[A-Z]+")]
        [InlineData("3498792", @"\d+")]
        public void ReturnsExpectedValueGivenCorrectFormat(string input,string regexPattern)
        {
            var result = Guard.Against.InvalidFormat(input, nameof(input), regexPattern);
            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData("aaa", @"\d{1,6}")]
        [InlineData("50XA", @"[0-9a-fA-F]{1,6}")]
        [InlineData("2GudhUtG", @"[a-fA-F]+")]
        [InlineData("sDHSTRY", @"[A-Z]+")]
        [InlineData("3F498792", @"\d+")]
        [InlineData("", @"\d+")]
        public void ThrowsGivenGivenIncorrectFormat(string input, string regexPattern)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.InvalidFormat(input, nameof(input), regexPattern));
        }

        [Theory]
        [InlineData(null, "Input parameterName was not in required format (Parameter 'parameterName')")]
        [InlineData("Please provide value in a correct format", "Please provide value in a correct format (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.InvalidFormat("aaa", "parameterName", "^b", customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "Please provide correct value")]
        [InlineData("SomeParameter", null)]
        [InlineData("SomeOtherParameter", "Value must be correct")]
        public void ExceptionParamNameMatchesExpected(string expectedParamName, string customMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.InvalidFormat("aaa", expectedParamName, "^b", customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }
    }
}
