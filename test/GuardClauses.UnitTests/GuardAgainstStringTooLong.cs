using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests;

public class GuardAgainstStringTooLong
{
    [Theory]
    [InlineData("", 3, "string")]
    [InlineData("a", 3, "string")]
    [InlineData("ab", 3, "string")]
    [InlineData("abc", 3, "string")]
    public void DoesNothingGivenNonEmptyString(string input, int maxLength, string parameterName)
    {
        Guard.Against.StringTooLong(input, maxLength, parameterName);
    }

    [Theory]
    [InlineData("a", 0, "string")]
    [InlineData("a", -1, "string")]
    public void ThrowsGivenNonPositiveMaxLength(string input, int maxLength, string parameterName)
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.StringTooLong(input, maxLength, parameterName));
    }

    [Fact]
    public void ThrowsGivenStringLongerThanMaxLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.StringTooLong("aaa", 2, "string"));
    }

    [Fact]
    public void ReturnsExpectedValueWhenGivenAStringShorterThanMaxLength()
    {
        var actual = Guard.Against.StringTooLong("aaa", 3, "string");
        Assert.Equal("aaa", actual);
    }
}
