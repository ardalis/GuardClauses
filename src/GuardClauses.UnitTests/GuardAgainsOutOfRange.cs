using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainsOutOfRange
    {
        [Theory]
        [InlineData(1, 1, 5)]
        [InlineData(2, 1, 5)]
        [InlineData(3, 1, 5)]
        public void DoesNothingGivenInRangeValueUsingShortcutMethod(int input, int rangeFrom, int rangeTo)
        {
            Guard.AgainsOutOfRange(input, "index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(1, 1, 5)]
        [InlineData(2, 1, 5)]
        [InlineData(3, 1, 5)]
        public void DoesNothingGivenInRangeValueUsingExtensionMethod(int input, int rangeFrom, int rangeTo)
        {
            Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(-1, 1, 5)]
        [InlineData(0, 1, 5)]
        [InlineData(6, 1, 5)]
        public void ThrowsGivenOutOfRangeValueUsingShortcutMethod(int input, int rangeFrom, int rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.AgainsOutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(-1, 1, 5)]
        [InlineData(0, 1, 5)]
        [InlineData(6, 1, 5)]
        public void ThrowsGivenOutOfRangeValueUsingExtensionMethod(int input, int rangeFrom, int rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }
    }
}