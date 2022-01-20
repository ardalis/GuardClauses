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
            Assert.Throws<ArgumentException>(() => Guard.Against.Foo("foo", "aParameterName"));
        }

        [Fact]
        public void DoesNothingGivenAnythingElse()
        {
            Guard.Against.Foo("anythingElse", "aParameterName");
            //Guard.Against.Foo(null, "aParameterName");
        }

        [Fact]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvided()
        {
            string? xyz = "foo";

            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Foo(xyz));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Contains($"Should not have been foo! (Parameter '{nameof(xyz)}')", exception.Message);
        }
    }
}
