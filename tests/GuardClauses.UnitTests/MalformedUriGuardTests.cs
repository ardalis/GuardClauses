using Ardalis.GuardClauses;

namespace GuardClauses.UnitTests;

public class MalformedUriGuardTests
{
    [Fact]
    public void DoesNothingGivenValidAbsoluteUri()
    {
        var result = Guard.Against.AgainstMalformedUri("https://example.com", "url");
        Assert.Equal("https://example.com", result);
    }

    [Fact]
    public void ThrowsGivenMalformedInput()
    {
        Assert.Throws<ArgumentException>(
            () => Guard.Against.AgainstMalformedUri("not-a-uri", "url"));
    }
}
