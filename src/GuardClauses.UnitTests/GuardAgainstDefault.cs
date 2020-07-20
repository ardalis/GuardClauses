using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstDefault
    {
        [Fact]
        public void DoesNothingGivenNonDefaultValue()
        {
            Guard.WithValue("").AgainstDefault("string");
            Guard.WithValue(1).AgainstDefault("int");
            Guard.WithValue(Guid.NewGuid()).AgainstDefault("guid");
            Guard.WithValue(DateTime.Now).AgainstDefault("datetime");
            Guard.WithValue(new object()).AgainstDefault("object");
        }

        [Fact]
        public void ThrowsGivenDefaultValue()
        {
            Assert.Throws<ArgumentException>("string", () => Guard.WithValue(default(string)).AgainstDefault("string"));
            Assert.Throws<ArgumentException>("int", () => Guard.WithValue(default(int)).AgainstDefault("int"));
            Assert.Throws<ArgumentException>("guid", () => Guard.WithValue(default(Guid)).AgainstDefault("guid"));
            Assert.Throws<ArgumentException>("datetime", () => Guard.WithValue(default(DateTime)).AgainstDefault( "datetime"));
            Assert.Throws<ArgumentException>("object", () => Guard.WithValue(default(object)).AgainstDefault("object"));
        }

        [Theory]
        [MemberData(nameof(GetNonDefaultTestVectors))]
        public void ReturnsExpectedValueWhenGivenNonDefaultValue(object input, string name, object expected)
        {
            var actual = Guard.WithValue(input).AgainstDefault(name).Value;
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetNonDefaultTestVectors()
        {
            yield return new object[] {"", "string", ""};
            yield return new object[] {1, "int", 1};
            
            var guid = Guid.NewGuid();
            yield return new object[] {guid, "guid", guid};

            var now = DateTime.Now;
            yield return new object[] {now, "now", now};

            var obj = new Object();
            yield return new object[] {obj, "obj", obj};
        }
    }
}
