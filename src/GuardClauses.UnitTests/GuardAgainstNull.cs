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
            Guard.WithValue("").AgainstNull("string");
            Guard.WithValue(1).AgainstNull("int");
            Guard.WithValue(Guid.Empty).AgainstNull("guid");
            Guard.WithValue(DateTime.Now).AgainstNull("datetime");
            Guard.WithValue(new object()).AgainstNull("object");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.WithValue<string>(null!).AgainstNull("null"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenNonNullValue()
        {
            Assert.Equal("", Guard.WithValue("").AgainstNull("string").Value);
            Assert.Equal(1, Guard.WithValue(1).AgainstNull("int").Value);

            var guid = Guid.Empty;
            Assert.Equal(guid, Guard.WithValue(guid).AgainstNull("guid").Value);
            
            var now = DateTime.Now;
            Assert.Equal(now, Guard.WithValue(now).AgainstNull("datetime").Value);

            var obj = new Object();
            Assert.Equal(obj, Guard.WithValue(obj).AgainstNull("object").Value);
        }
    }
}
