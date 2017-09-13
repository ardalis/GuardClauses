using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNull
    {
        [Fact]
        public void DoesNothingGivenNonNullValueUsingShortcutMethod()
        {
            Guard.AgainstNull("", "string");
            Guard.AgainstNull(1, "int");
            Guard.AgainstNull(Guid.Empty, "guid");
            Guard.AgainstNull(DateTime.Now, "datetime");
            Guard.AgainstNull(new Object(), "object");
        }

        [Fact]
        public void DoesNothingGivenNonNullValueUsingSpecificMethodPath()
        {
            Guard.Against.Null("", "string");
            Guard.Against.Null(1, "int");
            Guard.Against.Null(Guid.Empty, "guid");
            Guard.Against.Null(DateTime.Now, "datetime");
            Guard.Against.Null(new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenNullValueUsingShortcutMethod()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.AgainstNull(null, "null"));
        }

        [Fact]
        public void ThrowsGivenNullValueUsingSpecificMethodPath()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.Against.Null(null, "null"));
        }
    }
}
