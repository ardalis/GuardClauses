using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests;

public class GuardAgainstStringLengthOutOfRange
{
    [Theory]
    [InlineData("a")]
    [InlineData("abcd")]
    public void PreservesCustomMessageAndParameterName(string input)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            Guard.Against.LengthOutOfRange(input, 2, 3, "username", "Use 2 to 3 characters."));

        Assert.Equal("username", exception.ParamName);
        Assert.StartsWith("Use 2 to 3 characters.", exception.Message);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("abcd")]
    public void InfersInputParameterName(string input)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            Guard.Against.LengthOutOfRange(input, 2, 3));

        Assert.Equal(nameof(input), exception.ParamName);
    }

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
    public void ThrowsCustomExceptionWhenSuppliedGivenEmptyString()
    {
        Exception customException = new Exception();
        Assert.Throws<Exception>(() => Guard.Against.LengthOutOfRange("", 1, 2, "string",
            exceptionCreator: () => customException));
    }

    [Fact]
    public void ThrowsGivenNonPositiveMinLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("", 0, 0, "string"));
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("", -1, -1, "string"));
    }

    [Fact]
    public void ThrowsCustomExceptionWhenSuppliedGivenNonPositiveMinLength()
    {
        Exception customException = new Exception();
        Assert.Throws<Exception>(() => Guard.Against.LengthOutOfRange("", 0, 0, "string",
            exceptionCreator: () => customException));
        Assert.Throws<Exception>(() => Guard.Against.LengthOutOfRange("", -1, -1, "string",
            exceptionCreator: () => customException));
    }

    [Fact]
    public void ThrowsGivenStringShorterThanMinLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("a", 2, 2, "string"));
    }

    [Fact]
    public void ThrowsCustomExceptionWhenSuppliedGivenStringShorterThanMinLength()
    {
        Exception customException = new Exception();
        Assert.Throws<Exception>(() => Guard.Against.LengthOutOfRange("a", 2, 2, "string",
            exceptionCreator: () => customException));
    }

    [Fact]
    public void ThrowsGivenStringLongerThanMaxLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("abcd", 2, 2, "string"));
    }

    [Fact]
    public void ThrowsCustomExceptionWhenSuppliedGivenStringLongerThanMaxLength()
    {
        Exception customException = new Exception();
        Assert.Throws<Exception>(() => Guard.Against.LengthOutOfRange("abcd", 2, 2, "string",
            exceptionCreator: () => customException));
    }

    [Fact]
    public void ThrowsWhenMinIsBiggerThanMax()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.LengthOutOfRange("asd", 4, 2, "string"));
    }

    [Fact]
    public void ThrowsCustomExceptionWhenSuppliedWhenMinIsBiggerThanMax()
    {
        Exception customException = new Exception();
        Assert.Throws<Exception>(() => Guard.Against.LengthOutOfRange("asd", 4, 2, "string",
            exceptionCreator: () => customException));
    }


    [Fact]
    public void ReturnsExpectedValueWhenGivenLongerString()
    {
        var expected = "abc";
        var actual = Guard.Against.LengthOutOfRange("abc", 2, 5, "string");
        Assert.Equal(expected, actual);
    }
}
