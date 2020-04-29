using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForDouble
    {
        [Theory]
        [InlineData(1.0, 1.0, 1.0)]
        [InlineData(1.0, 1.0, 3.0)]
        [InlineData(2.0, 1.0, 3.0)]
        [InlineData(3.0, 1.0, 3.0)]
        public void DoesNothingGivenInRangeValue(double input, double rangeFrom, double rangeTo)
        {
            Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(-1.0, 1.0, 3.0)]
        [InlineData(0.0, 1.0, 3.0)]
        [InlineData(4.0, 1.0, 3.0)]
        public void ThrowsGivenOutOfRangeValue(double input, double rangeFrom, double rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(-1.0, 3.0, 1.0)]
        [InlineData(0.0, 3.0, 1.0)]
        [InlineData(4.0, 3.0, 1.0)]
        public void ThrowsGivenInvalidArgumentValue(double input, double rangeFrom, double rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(1.0, 0.0, 1.0, 1.0)]
        [InlineData(1.0, 0.0, 3.0, 1.0)]
        [InlineData(2.0, 1.0, 3.0, 2.0)]
        [InlineData(3.0, 3.0, 3.0, 3.0)]
        public void ReturnsExpectedValueGivenInRangeValue(double input, double rangeFrom, double rangeTo, double expected)
        {
            Assert.Equal(expected, Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }
    }
}
