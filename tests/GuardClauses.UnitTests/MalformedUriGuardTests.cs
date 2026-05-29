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
    public void DoesNothingGivenHttpUri()
    {
        var result = Guard.Against.AgainstMalformedUri("http://example.com", "url");
        Assert.Equal("http://example.com", result);
    }

    [Fact]
    public void ThrowsGivenFtpUri()
    {
        Assert.Throws<ArgumentException>(
            () => Guard.Against.AgainstMalformedUri("ftp://example.com", "url"));
    }

    [Fact]
    public void ThrowsGivenNullInput()
    {
        Assert.Throws<ArgumentNullException>(
            () => Guard.Against.AgainstMalformedUri(null!, "url"));
    }

    [Fact]
    public void ThrowsGivenMalformedInput()
    {
        Assert.Throws<ArgumentException>(
            () => Guard.Against.AgainstMalformedUri("not-a-uri", "url"));
    }
}
