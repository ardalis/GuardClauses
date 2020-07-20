using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstFooExtension
    {
        [Fact]
        public void ThrowsGivenFoo()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue("foo").AgainstFoo("aParameterName"));
        }

        [Fact]
        public void DoesNothingGivenAnythingElse()
        {
            Guard.WithValue("anythingElse").AgainstFoo("aParameterName");
            Guard.WithValue<string>(null!).AgainstFoo("aParameterName");
        }
    }
}
