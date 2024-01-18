﻿using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests;

public class GuardAgainstStringTooShort
{
    [Fact]
    public void DoesNothingGivenNonEmptyString()
    {
        Guard.Against.StringTooShort("a", 1, "string");
        Guard.Against.StringTooShort("abc", 1, "string");
        Guard.Against.StringTooShort("a", 1, "string");
        Guard.Against.StringTooShort("a", 1, "string");
        Guard.Against.StringTooShort("a", 1, "string");
    }

    [Fact]
    public void ThrowsGivenEmptyString()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.StringTooShort("", 1, "string"));
    }

    [Fact]
    public void ThrowsGivenNonPositiveMinLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.StringTooShort("", 0, "string"));
        Assert.Throws<ArgumentException>(() => Guard.Against.StringTooShort("", -1, "string"));
    }

    [Fact]
    public void ThrowsGivenStringShorterThanMinLength()
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.StringTooShort("a", 2, "string"));
    }

    [Fact]
    public void ReturnsExpectedValueWhenGivenLongerString()
    {
        var expected = "abc";
        var actual = Guard.Against.StringTooShort("abc", 2, "string");
        Assert.Equal(expected, actual);
    }
}
