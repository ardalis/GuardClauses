using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForDateTime
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 3)]
        [InlineData(-1, 1)]
        [InlineData(-1, 0)]
        public void DoesNothingGivenInRangeValue(int rangeFromOffset, int rangeToOffset)
        {
            DateTime input = DateTime.Now;
            DateTime rangeFrom = input.AddSeconds(rangeFromOffset);
            DateTime rangeTo = input.AddSeconds(rangeToOffset);
            Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(-4, -3)]
        public void ThrowsGivenOutOfRangeValue(int rangeFromOffset, int rangeToOffset)
        {
            DateTime input = DateTime.Now;
            DateTime rangeFrom = input.AddSeconds(rangeFromOffset);
            DateTime rangeTo = input.AddSeconds(rangeToOffset);
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(3, -1)]
        public void ThrowsGivenInvalidArgumentValue(int rangeFromOffset, int rangeToOffset)
        {
            DateTime input = DateTime.Now;
            DateTime rangeFrom = input.AddSeconds(rangeFromOffset);
            DateTime rangeTo = input.AddSeconds(rangeToOffset);
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(DateTime.Now, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 3)]
        [InlineData(-1, 1)]
        [InlineData(-1, 0)]
        public void ReturnsExpectedValueGivenInRangeValue(int rangeFromOffset, int rangeToOffset)
        {
            DateTime input = DateTime.Now;
            DateTime expected = input;
            DateTime rangeFrom = input.AddSeconds(rangeFromOffset);
            DateTime rangeTo = input.AddSeconds(rangeToOffset);
            Assert.Equal(expected, Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(null, "Input parameterName was out of range (Parameter 'parameterName')")]
        [InlineData("DateTime range", "DateTime range (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(DateTime.Today.AddDays(-1), "parameterName",
                DateTime.Today, DateTime.Today.AddDays(1), customMessage));
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
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(DateTime.Today.AddDays(-1), expectedParamName,
                DateTime.Today, DateTime.Today.AddDays(1), customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }
    }
}
