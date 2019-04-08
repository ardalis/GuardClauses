using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstDefault
    {
        [Fact]
        public void DoesNothingGivenNonDefaultValue()
        {
            Guard.Against.Default("", "string");
            Guard.Against.Default(1, "int");
            Guard.Against.Default(Guid.NewGuid(), "guid");
            Guard.Against.Default(DateTime.Now, "datetime");
            Guard.Against.Default(new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenDefaultValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Default(default(string), "string"));
            Assert.Throws<ArgumentException>(() => Guard.Against.Default(default(int), "int"));
            Assert.Throws<ArgumentException>(() => Guard.Against.Default(default(Guid), "guid"));
            Assert.Throws<ArgumentException>(() => Guard.Against.Default(default(DateTime), "datetime"));
            Assert.Throws<ArgumentException>(() => Guard.Against.Default(default(object), "object"));
        }
    }
}
