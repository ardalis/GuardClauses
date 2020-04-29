using Ardalis.GuardClauses;
using System;
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


    }
}
