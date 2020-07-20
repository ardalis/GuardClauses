using Ardalis.GuardClauses;
using System;
using System.Linq;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNullOrEmpty
    {
        [Fact]
        public void DoesNothingGivenNonEmptyStringValue()
        {
            Guard.WithValue("a").AgainstNullOrEmpty("string");
            Guard.WithValue("1").AgainstNullOrEmpty("aNumericString");
        }

        [Fact]
        public void DoesNothingGivenNonEmptyGuidValue()
        {
            Guard.WithValue(Guid.NewGuid()).AgainstEmpty("guid");
        }

        [Fact]
        public void DoesNothingGivenNonEmptyEnumerable()
        {
            Guard.WithValue(new[] { "foo", "bar" }).AgainstNullOrEmpty("stringArray");
            Guard.WithValue(new[] { 1, 2 }).AgainstNullOrEmpty("intArray");
        }

        [Fact]
        public void ThrowsGivenNullString()
        {
            string? nullString = null;
            Assert.Throws<ArgumentNullException>(() => Guard.WithValue(nullString!).AgainstNullOrEmpty("nullString"));
        }

        [Fact]
        public void ThrowsGivenEmptyString()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue("").AgainstNullOrEmpty("emptyString"));
        }

        [Fact]
        public void ThrowsGivenNullGuid()
        {
            Guid? nullGuid = null;
            Assert.Throws<ArgumentNullException>(() => Guard.WithValue(nullGuid).AgainstNullOrEmpty("nullGuid"));
        }

        [Fact]
        public void ThrowsGivenEmptyGuid()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(Guid.Empty).AgainstEmpty("emptyGuid"));
        }

        [Fact]
        public void ThrowsGivenEmptyEnumerable()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(Enumerable.Empty<string>()).AgainstNullOrEmpty("emptyStringEnumerable"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenValidValue()
        {
            Assert.Equal("a", Guard.WithValue("a").AgainstNullOrEmpty("string").Value);
            Assert.Equal("1", Guard.WithValue("1").AgainstNullOrEmpty("aNumericString").Value);

            var collection1 = new[] {"foo", "bar"};
            Assert.Equal(collection1, Guard.WithValue(collection1).AgainstNullOrEmpty("stringArray").Value);

            var collection2 = new[] {1, 2};
            Assert.Equal(collection2, Guard.WithValue(collection2).AgainstNullOrEmpty("intArray").Value);
        }
    }
}
