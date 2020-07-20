using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForShort
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(3, 1, 3)]
        public void DoesNothingGivenInRangeValue(short input, short rangeFrom, short rangeTo)
        {
            Guard.WithValue(input).AgainstOutOfRange("index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(-1, 1, 3)]
        [InlineData(0, 1, 3)]
        [InlineData(4, 1, 3)]
        public void ThrowsGivenOutOfRangeValue(short input, short rangeFrom, short rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.WithValue(input).AgainstOutOfRange("index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(-1, 3, 1)]
        [InlineData(0, 3, 1)]
        [InlineData(4, 3, 1)]
        public void ThrowsGivenInvalidArgumentValue(short input, short rangeFrom, short rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(input).AgainstOutOfRange("index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(1, 1, 3, 1)]
        [InlineData(2, 1, 3, 2)]
        [InlineData(3, 1, 3, 3)]
        public void ReturnsExpectedValueGivenInRangeValue(short input, short rangeFrom, short rangeTo, short expected)
        {
            Assert.Equal(expected, Guard.WithValue(input).AgainstOutOfRange("index", rangeFrom, rangeTo).Value);
        }
    }
}
