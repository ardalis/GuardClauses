using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNull
    {
        [Fact]
        public void DoesNothingGivenNonNullValue()
        {
            Guard.Against.Null("", "string");
            Guard.Against.Null(1, "int");
            Guard.Against.Null(Guid.Empty, "guid");
            Guard.Against.Null(DateTime.Now, "datetime");
            Guard.Against.Null(new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            object obj = null!;
            Assert.Throws<ArgumentNullException>(() => Guard.Against.Null(obj, "null"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenNonNullValue()
        {
            Assert.Equal("", Guard.Against.Null("", "string"));
            Assert.Equal(1, Guard.Against.Null(1, "int"));

            var guid = Guid.Empty;
            Assert.Equal(guid, Guard.Against.Null(guid, "guid"));

            var now = DateTime.Now;
            Assert.Equal(now, Guard.Against.Null(now, "datetime"));

            var obj = new Object();
            Assert.Equal(obj, Guard.Against.Null(obj, "object"));
        }

        [Theory]
        [InlineData(null, "Value cannot be null. (Parameter 'parameterName')")]
        [InlineData("Please provide value", "Please provide value")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            string? nullString = null;
            var exception = Assert.Throws<ArgumentNullException>(() => Guard.Against.Null(nullString, "parameterName", customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvided()
        {
            string? xyz = null;

            var exception = Assert.Throws<ArgumentNullException>(() => Guard.Against.Null(xyz));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Contains($"Value cannot be null. (Parameter '{nameof(xyz)}')", exception.Message);
        }
    }
}
