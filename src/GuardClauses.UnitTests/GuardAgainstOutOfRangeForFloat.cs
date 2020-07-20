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
            Guard.WithValue(input).AgainstOutOfRange("index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(-1.0, 1.0, 3.0)]
        [InlineData(0.0, 1.0, 3.0)]
        [InlineData(4.0, 1.0, 3.0)]
        public void ThrowsGivenOutOfRangeValue(float input, float rangeFrom, float rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.WithValue(input).AgainstOutOfRange("index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(-1.0, 3.0, 1.0)]
        [InlineData(0.0, 3.0, 1.0)]
        [InlineData(4.0, 3.0, 1.0)]
        public void ThrowsGivenInvalidArgumentValue(float input, float rangeFrom, float rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(input).AgainstOutOfRange("index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0, 1.0)]
        [InlineData(1.0, 1.0, 3.0, 1.0)]
        [InlineData(2.0, 1.0, 3.0, 2.0)]
        [InlineData(3.0, 1.0, 3.0, 3.0)]
        public void ReturnsExpectedValueGivenInRangeValue(float input, float rangeFrom, float rangeTo, float expected)
        {
            Assert.Equal(expected, Guard.WithValue(input).AgainstOutOfRange("index", rangeFrom, rangeTo).Value);
        }
    }
}
