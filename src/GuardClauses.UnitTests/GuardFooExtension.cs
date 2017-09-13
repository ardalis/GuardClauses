using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstFoo
    {
        [Fact]
        public void DoesNothingGivenFoo()
        {
            Guard.Against.Null("", "string");
            Guard.Against.Null(1, "int");
            Guard.Against.Null(Guid.Empty, "guid");
            Guard.Against.Null(DateTime.Now, "datetime");
            Guard.Against.Null(new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenFoo()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Foo("foo", "aParameterName"));
        }

        [Fact]
        public void DoesNothingGivenAnythingElse()
        {
            Guard.Against.Foo("anythingElse", "aParameterName");
            Guard.Against.Foo(null, "aParameterName");
        }
    }
}
