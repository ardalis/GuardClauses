using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests;

public class GuardAgainstStringTooLong
{
    [Fact]
    public void DoesNothingGivenNonEmptyString()
    {
        Guard.Against.StringTooLong("a", 3, "string");
        Guard.Against.StringTooLong("abc", 3, "string");
        Guard.Against.StringTooLong("a", 3, "string");
        Guard.Against.StringTooLong("a", 3, "string");
        Guard.Against.StringTooLong("a", 3, "string");
    }

    [Fact]
    public void ThrowsGivenNonPositiveMaxLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.StringTooLong("a", 0, "string"));
        Assert.Throws<ArgumentException>(() => Guard.Against.StringTooLong("a", -1, "string"));
    }

    [Fact]
    public void ThrowsCustomExceptionWhenSuppliedGivenNonPositiveMaxLength()
    {
        Exception customException = new Exception();
        Assert.Throws<Exception>(() => Guard.Against.StringTooLong("a", 0, "string", exceptionCreator: () => customException));
        Assert.Throws<Exception>(() => Guard.Against.StringTooLong("a", -1, "string", exceptionCreator: () => customException));
    }

    [Fact]
    public void ThrowsGivenStringLongerThanMaxLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.StringTooLong("abc", 2, "string"));
    }

    [Fact]
    public void ThrowsCustomExceptionWhenSuppliedGivenStringLongerThanMaxLength()
    {
        Exception customException = new Exception();
        Assert.Throws<Exception>(() => Guard.Against.StringTooLong("abc", 2, "string", exceptionCreator: () => customException));
    }

    [Fact]
    public void ReturnsExpectedValueWhenGivenValidString()
    {
        var expected = "abc";
        var actual = Guard.Against.StringTooLong("abc", 3, "string");
        Assert.Equal(expected, actual);
    }
}
