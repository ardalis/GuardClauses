using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForInt
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(3, 1, 3)]
        public void DoesNothingGivenInRangeValue(int input, int rangeFrom, int rangeTo)
        {
            Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(-1, 1, 3)]
        [InlineData(0, 1, 3)]
        [InlineData(4, 1, 3)]
        public void ThrowsGivenOutOfRangeValue(int input, int rangeFrom, int rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(-1, 3, 1)]
        [InlineData(0, 3, 1)]
        [InlineData(4, 3, 1)]
        public void ThrowsGivenInvalidArgumentValue(int input, int rangeFrom, int rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(1, 1, 3, 1)]
        [InlineData(2, 1, 3, 2)]
        [InlineData(3, 1, 3, 3)]
        public void ReturnsExpectedValueGivenInRangeValue(int input, int rangeFrom, int rangeTo, int expected)
        {
            Assert.Equal(expected, Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }
    }
}
