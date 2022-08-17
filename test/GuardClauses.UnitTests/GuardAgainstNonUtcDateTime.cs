using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNonUtcDateTime
    {
        [Fact]
        public void DoesNothingGivenUtcKind()
        {
            Guard.Against.NonUtcDateTime(DateTime.UtcNow, "UtcNow");
            Guard.Against.NonUtcDateTime(new DateTime(2001, 12, 22, 21, 37, 55, DateTimeKind.Utc), "new DateTime()");
        }

        [Fact]
        public void ThrowsGivenLocalKind()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NonUtcDateTime(DateTime.Now, "Now"));
            Assert.Throws<ArgumentException>(() => 
                Guard.Against.NonUtcDateTime(new DateTime(2001, 12, 22, 21, 37, 55, DateTimeKind.Local), "new DateTime()"));
        }

        [Fact]
        public void ThrowsGivenUnspecifiedKind()
        {
            Assert.Throws<ArgumentException>(() =>
                Guard.Against.NonUtcDateTime(new DateTime(2001, 12, 22, 21, 37, 55, DateTimeKind.Unspecified), "new DateTime()"));
        }
    }
}
