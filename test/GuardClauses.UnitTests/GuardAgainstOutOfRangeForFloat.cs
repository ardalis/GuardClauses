using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForFloat
    {
        [Theory]
        [InlineData(1.0, 1.0, 1.0)]
        [InlineData(1.0, 1.0, 3.0)]
        [InlineData(2.0, 1.0, 3.0)]
        [InlineData(3.0, 1.0, 3.0)]
        public void DoesNothingGivenInRangeValue(float input, float rangeFrom, float rangeTo)
        {
            Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(-1.0, 1.0, 3.0)]
        [InlineData(0.0, 1.0, 3.0)]
        [InlineData(4.0, 1.0, 3.0)]
        public void ThrowsGivenOutOfRangeValue(float input, float rangeFrom, float rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(-1.0, 3.0, 1.0)]
        [InlineData(0.0, 3.0, 1.0)]
        [InlineData(4.0, 3.0, 1.0)]
        public void ThrowsGivenInvalidArgumentValue(float input, float rangeFrom, float rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0, 1.0)]
        [InlineData(1.0, 1.0, 3.0, 1.0)]
        [InlineData(2.0, 1.0, 3.0, 2.0)]
        [InlineData(3.0, 1.0, 3.0, 3.0)]
        public void ReturnsExpectedValueGivenInRangeValue(float input, float rangeFrom, float rangeTo, float expected)
        {
            Assert.Equal(expected, Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(null, "Input parameterName was out of range (Parameter 'parameterName')")]
        [InlineData("Float range", "Float range")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(3.0f, "parameterName", 0.0f, 1.0f, customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
