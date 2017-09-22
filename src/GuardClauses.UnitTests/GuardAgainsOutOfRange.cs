using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainsOutOfRange
    {
        [Fact]
        public void DoesNothingGivenInRangeValueUsingShortcutMethod()
        {
            Guard.AgainsOutOfRange(2, "index", 1, 5);
        }

        [Fact]
        public void DoesNothingGivenInRangeValueUsingExtensionMethod()
        {
            Guard.Against.OutOfRange(2, "index", 1, 5);
        }

        [Fact]
        public void ThrowsGivenOutOfRangeValueUsingShortcutMethod()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.AgainsOutOfRange(5, "index", 1, 4));
        }

        [Fact]
        public void ThrowsGivenOutOfRangeValueUsingExtensionMethod()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(5, "index", 1, 4));
        }
    }
}