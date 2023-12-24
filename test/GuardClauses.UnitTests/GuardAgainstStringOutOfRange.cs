using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests;

public class GuardAgainstStringOutOfRange
{
    [Fact]
    public void DoesNothingGivenNonEmptyString()
    {
        Guard.Against.LengthOutOfRange("a", 1, 4, "string");
        Guard.Against.LengthOutOfRange("abc", 1, 4, "string");
        Guard.Against.LengthOutOfRange("a", 1, 4, "string");
        Guard.Against.LengthOutOfRange("a", 1, 4, "string");
        Guard.Against.LengthOutOfRange("a", 1, 4, "string");
    }

    [Fact]
    public void ThrowsGivenEmptyString()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("", 1, 2, "string"));
    }

    [Fact]
    public void ThrowsGivenNonPositiveMinLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("", 0, 0, "string"));
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("", -1, -1, "string"));
    }

    [Fact]
    public void ThrowsGivenStringShorterThanMinLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("a", 2, 2, "string"));
    }

    [Fact]
    public void ThrowsGivenStringLongerThanMaxLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("abcd", 2, 2, "string"));
    }

    [Fact]
    public void ReturnsExpectedValueWhenGivenLongerString()
    {
        var expected = "abc";
        var actual = Guard.Against.LengthOutOfRange("abc", 2, 5, "string");
        Assert.Equal(expected, actual);
    }
}
