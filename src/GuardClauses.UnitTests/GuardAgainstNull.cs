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
            Guard.Against.Null("", "string");
            Guard.Against.Null(1, "int");
            Guard.Against.Null(Guid.Empty, "guid");
            Guard.Against.Null(DateTime.Now, "datetime");
            Guard.Against.Null(new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            object obj = null!;
            Assert.Throws<ArgumentNullException>(() => Guard.Against.Null(obj, "null"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenNonNullValue()
        {
            Assert.Equal("", Guard.Against.Null("", "string"));
            Assert.Equal(1, Guard.Against.Null(1, "int"));

            var guid = Guid.Empty;
            Assert.Equal(guid, Guard.Against.Null(guid, "guid"));
            
            var now = DateTime.Now;
            Assert.Equal(now, Guard.Against.Null(now, "datetime"));

            var obj = new Object();
            Assert.Equal(obj, Guard.Against.Null(obj, "object"));
        }
    }
}
