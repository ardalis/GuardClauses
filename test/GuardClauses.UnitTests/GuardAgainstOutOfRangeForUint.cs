using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForUint
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(3, 1, 3)]
        public void DoesNothingGivenInRangeValue(uint input, uint rangeFrom, uint rangeTo)
        {
            Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(0, 1, 3)]
        [InlineData(4, 1, 3)]
        public void ThrowsGivenOutOfRangeValue(uint input, uint rangeFrom, uint rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(0, 3, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(4, 3, 1)]
        public void ThrowsGivenInvalidArgumentValue(uint input, uint rangeFrom, uint rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(1, 1, 3, 1)]
        [InlineData(2, 1, 3, 2)]
        [InlineData(3, 1, 3, 3)]
        public void ReturnsExpectedValueGivenInRangeValue(uint input, uint rangeFrom, uint rangeTo, uint expected)
        {
            Assert.Equal(expected, Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(null, "Input parameterName was out of range (Parameter 'parameterName')")]
        [InlineData("Uint range", "Uint range (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange((uint)3.0d, "parameterName", (uint)0.0d, (uint)1.0d, customMessage));
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
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange((uint)3.0d, expectedParamName, (uint)0.0d, (uint)1.0d, customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }
    }
}
