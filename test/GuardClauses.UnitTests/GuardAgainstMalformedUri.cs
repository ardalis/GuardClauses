using System;
using Xunit;

namespace Ardalis.GuardClauses.UnitTests;

public class GuardAgainstMalformedUri
{
    [Fact]
    public void DoesNothingGivenValidAbsoluteUri()
    {
        var result = Guard.Against.MalformedUri("https://example.com");

        Assert.Equal("https://example.com", result);
    }

    [Fact]
    public void ThrowsGivenMalformedUri()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            Guard.Against.MalformedUri("not a uri"));

        Assert.Contains("valid absolute URI", exception.Message);
    }
}
