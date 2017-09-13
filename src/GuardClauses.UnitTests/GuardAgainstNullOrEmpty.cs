using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNullOrEmpty
    {
        [Fact]
        public void DoesNothingGivenNonEmptyStringValueUsingShortcutMethod()
        {
            Guard.AgainstNullOrEmpty("a", "string");
            Guard.AgainstNullOrEmpty("1", "aNumericString");
        }

        [Fact]
        public void DoesNothingGivenNonEmptyStringValueUsingSpecificMethodPath()
        {
            Guard.Against.NullOrEmpty("a", "string");
            Guard.Against.NullOrEmpty("1", "aNumericString");
        }

        [Fact]
        public void ThrowsGivenNullValueUsingShortcutMethod()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.AgainstNullOrEmpty(null, "null"));
        }

        [Fact]
        public void ThrowsGivenEmptyStringUsingShortcutMethod()
        {
            Assert.Throws<ArgumentException>(() => Guard.AgainstNullOrEmpty("", "emptystring"));
        }

        [Fact]
        public void ThrowsGivenNullValueUsingSpecificMethodPath()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrEmpty(null, "null"));
        }

        [Fact]
        public void ThrowsGivenEmptyStringUsingSpecificMethodPath()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty("", "emptystring"));
        }
    }
}
