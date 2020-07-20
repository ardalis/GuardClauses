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
            Guard.WithValue(0).AgainstNegative("intZero");
            Guard.WithValue(0L).AgainstNegative("longZero");
            Guard.WithValue(0.0M).AgainstNegative("decimalZero");
            Guard.WithValue(0.0F).AgainstNegative("floatZero");
            Guard.WithValue(0.0).AgainstNegative("doubleZero");
        }
        
        [Fact]
        public void DoesNothingGivenPositiveValue()
        {
            Guard.WithValue(1).AgainstNegative("intZero");
            Guard.WithValue(1L).AgainstNegative("longZero");
            Guard.WithValue(1.0M).AgainstNegative("decimalZero");
            Guard.WithValue(1.0F).AgainstNegative("floatZero");
            Guard.WithValue(1.0).AgainstNegative("doubleZero");
        }
        
        [Fact]
        public void ThrowsGivenNegativeIntValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1).AgainstNegative("negative"));
        }

        [Fact]
        public void ThrowsGivenNegativeLongValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1L).AgainstNegative("negative"));
        }

        [Fact]
        public void ThrowsGivenNegativeDecimalValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1.0M).AgainstNegative("negative"));
        }

        [Fact]
        public void ThrowsGivenNegativeFloatValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1.0F).AgainstNegative("negative"));
        }

        [Fact]
        public void ThrowsGivenNegativeDoubleValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1.0).AgainstNegative("negative"));
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeIntValue()
        {
            Assert.Equal(0, Guard.WithValue(0).AgainstNegative("intZero").Value);
            Assert.Equal(1, Guard.WithValue(1).AgainstNegative("intOne").Value);
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeLongValue()
        {
            Assert.Equal(0L, Guard.WithValue(0L).AgainstNegative("longZero").Value);
            Assert.Equal(1L, Guard.WithValue(1L).AgainstNegative("longOne").Value);
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeDecimalValue()
        {
            Assert.Equal(0.0M, Guard.WithValue(0.0M).AgainstNegative("decimalZero").Value);
            Assert.Equal(1.0M, Guard.WithValue(1.0M).AgainstNegative("decimalOne").Value);
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeFloatValue()
        {
            Assert.Equal(0.0f, Guard.WithValue(0.0F).AgainstNegative("floatZero").Value);
            Assert.Equal(1.0f, Guard.WithValue(1.0F).AgainstNegative("floatOne").Value);
        }

        [Fact]
        public void ReturnsExpectedValueGivenNonNegativeDoubleValue()
        {
            Assert.Equal(0.0, Guard.WithValue(0.0).AgainstNegative("doubleZero").Value);
            Assert.Equal(1.0, Guard.WithValue(1.0).AgainstNegative("doubleOne").Value);
        }
    }
}