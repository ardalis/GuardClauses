using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNull
    {
        [Fact]
        public void DoesNothingGivenNonNullValue()
        {
            Guard.AgainstNull("", "string");
            Guard.AgainstNull(1, "int");
            Guard.AgainstNull(Guid.Empty, "guid");
            Guard.AgainstNull(DateTime.Now, "datetime");
            Guard.AgainstNull(new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.AgainstNull(null, "null"));
        }
    }
}
