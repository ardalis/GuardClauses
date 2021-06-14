using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNullOrDefault
    {
        [Fact]
        public void DoesNothingGivenNonNullValue()
        {
            Guard.Against.NullOrDefault("", "string");
            Guard.Against.NullOrDefault(1, "int");
            Guard.Against.NullOrDefault(Guid.NewGuid(), "guid");
            Guard.Against.NullOrDefault(DateTime.Now, "datetime");
            Guard.Against.NullOrDefault(new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            object obj = null!;
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrDefault(obj, "null"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenNonNullValue()
        {
            Assert.Equal("", Guard.Against.NullOrDefault("", "string"));
            Assert.Equal(1, Guard.Against.NullOrDefault(1, "int"));

            var guid = Guid.NewGuid();
            Assert.Equal(guid, Guard.Against.NullOrDefault(guid, "guid"));
            
            var now = DateTime.Now;
            Assert.Equal(now, Guard.Against.NullOrDefault(now, "datetime"));

            var obj = new Object();
            Assert.Equal(obj, Guard.Against.NullOrDefault(obj, "object"));
        }

        [Theory]
        [InlineData(null, "Value cannot be null. (Parameter 'parameterName')")]
        [InlineData("Please provide value", "Please provide value")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            string? nullString = null;
            var exception = Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrDefault(nullString, "parameterName", customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, "Required input parameterName was default value.")]
        [InlineData("Please provide value.", "Please provide value.")]
        public void ErrorMessageMatchesExpectedOnDefault(string customMessage, string expectedMessage)
        {
            Guid emptyGuid = Guid.Empty;
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.NullOrDefault(emptyGuid, "parameterName", customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage + " (Parameter 'parameterName')", exception.Message);
        }

        [Fact]
        public void GuidThrowsGivenDefaultValue()
        {
            Guid guid = Guid.Empty;
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrDefault(guid, "null"));
        }

        [Fact]
        public void DateTimeThrowsGivenDefaultValue()
        {
            DateTime date = DateTime.MinValue;
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrDefault(date, "null"));
        }

        [Fact]
        public void IntThrowsGivenDefaultValue()
        {
            int intValue = 0;
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrDefault(intValue, "null"));
        }
    }
}
