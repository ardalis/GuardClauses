using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstInvalidFormatTests
    {
        [Theory]
        [InlineData("12345",@"\d{1,6}")]
        [InlineData("50FA", @"[0-9a-fA-F]{1,6}")]
        [InlineData("abfACD", @"[a-fA-F]{1,8}")]
        [InlineData("DHSTRY",@"[A-Z]+")]
        [InlineData("3498792", @"\d+")]
        public void ReturnsExpectedValueGivenCorrectFormat(string input,string regexPattern)
        {
            var result = Guard.Against.InvalidFormat(input, nameof(input), regexPattern);
            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData("aaa", @"\d{1,6}")]
        [InlineData("50XA", @"[0-9a-fA-F]{1,6}")]
        [InlineData("2GudhUtG", @"[a-fA-F]+")]
        [InlineData("sDHSTRY", @"[A-Z]+")]
        [InlineData("3F498792", @"\d+")]
        public void ThrowsGivenGivenIncorrectFormat(string input, string regexPattern)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.InvalidFormat(input, nameof(input), regexPattern));
        }

    }
}
