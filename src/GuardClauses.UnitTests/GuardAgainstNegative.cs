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
        }
        
        [Fact]
        public void DoesNothingGivenPositiveValue()
        {
            Guard.Against.Negative(1, "intZero");
            Guard.Against.Negative(1L, "longZero");
            Guard.Against.Negative(1.0M, "decimalZero");
            Guard.Against.Negative(1.0f, "floatZero");
            Guard.Against.Negative(1.0, "doubleZero");
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
    }
}