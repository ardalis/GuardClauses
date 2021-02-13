using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNegative
    {
        [Fact]
        public void DoesNothingGivenZeroValue()
        {
            Guard.Against.Negative(0, "intZero");
            Guard.Against.Negative(0L, "longZero");
            Guard.Against.Negative(0.0M, "decimalZero");
            Guard.Against.Negative(0.0f, "floatZero");
            Guard.Against.Negative(0.0, "doubleZero");
            Guard.Against.Negative(TimeSpan.Zero, "timespanZero");
        }
        
        [Fact]
        public void DoesNothingGivenPositiveValue()
        {
            Guard.Against.Negative(1, "intZero");
            Guard.Against.Negative(1L, "longZero");
            Guard.Against.Negative(1.0M, "decimalZero");
            Guard.Against.Negative(1.0f, "floatZero");
            Guard.Against.Negative(1.0, "doubleZero");
            Guard.Against.Negative(TimeSpan.FromSeconds(1), "timespanZero");
        }
        
        [Fact]
        public void ThrowsGivenNegativeIntValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Negative(-1, "negative"));
        }

        [Fact]
        public void ThrowsGivenNegativeLongValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Negative(-1L, "negative"));
        }

        [Fact]
        public void ThrowsGivenNegativeDecimalValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Negative(-1.0M, "negative"));
        }

        [Fact]
        public void ThrowsGivenNegativeFloatValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Negative(-1.0f, "negative"));
        }

        [Fact]
        public void ThrowsGivenNegativeDoubleValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Negative(-1.0, "negative"));
        }

        [Fact]
        public void ThrowsGivenNegativeTimeSpanValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Negative(TimeSpan.FromSeconds(-1), "negative"));
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeIntValue()
        {
            Assert.Equal(0, Guard.Against.Negative(0, "intZero"));
            Assert.Equal(1, Guard.Against.Negative(1, "intOne"));
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeLongValue()
        {
            Assert.Equal(0L, Guard.Against.Negative(0L, "longZero"));
            Assert.Equal(1L, Guard.Against.Negative(1L, "longOne"));
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeDecimalValue()
        {
            Assert.Equal(0.0M, Guard.Against.Negative(0.0M, "decimalZero"));
            Assert.Equal(1.0M, Guard.Against.Negative(1.0M, "decimalOne"));
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeFloatValue()
        {
            Assert.Equal(0.0f, Guard.Against.Negative(0.0f, "floatZero"));
            Assert.Equal(1.0f, Guard.Against.Negative(1.0f, "floatOne"));
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeDoubleValue()
        {
            Assert.Equal(0.0, Guard.Against.Negative(0.0, "doubleZero"));
            Assert.Equal(1.0, Guard.Against.Negative(1.0, "doubleOne"));
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeTimeSpanValue()
        {
            Assert.Equal(TimeSpan.Zero, Guard.Against.Negative(TimeSpan.Zero, "timespanZero"));
            Assert.Equal(TimeSpan.FromSeconds(1), Guard.Against.Negative(TimeSpan.FromSeconds(1), "timespanOne"));
        }
    }
}
