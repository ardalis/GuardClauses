using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNegativeOrZero
    {
        [Fact]
        public void DoesNothingGivenPositiveValue() 
        {
            Guard.WithValue(1).AgainstNegativeOrZero("intPositive");
            Guard.WithValue(1L).AgainstNegativeOrZero("longPositive");
            Guard.WithValue(1.0M).AgainstNegativeOrZero("decimalPositive");
            Guard.WithValue(1.0F).AgainstNegativeOrZero("floatPositive");
            Guard.WithValue(1.0).AgainstNegativeOrZero("doublePositive");
        }

        [Fact]
        public void ThrowsGivenZeroIntValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0).AgainstNegativeOrZero("intZero"));
        }

        [Fact]
        public void ThrowsGivenZeroLongValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0L).AgainstNegativeOrZero("longZero"));
        }

        [Fact]
        public void ThrowsGivenZeroDecimalValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0M).AgainstNegativeOrZero("decimalZero"));
        }

        [Fact]
        public void ThrowsGivenZeroFloatValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0F).AgainstNegativeOrZero("floatZero"));
        }

        [Fact]
        public void ThrowsGivenZeroDoubleValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0.0).AgainstNegativeOrZero("doubleZero"));
        }


        [Fact]
        public void ThrowsGivenNegativeIntValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1).AgainstNegativeOrZero("intNegative"));
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-42).AgainstNegativeOrZero("intNegative"));
        }

        [Fact]
        public void ThrowsGivenNegativeLongValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1L).AgainstNegativeOrZero("longNegative"));
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-456L).AgainstNegativeOrZero("longNegative"));
        }

        [Fact]
        public void ThrowsGivenNegativeDecimalValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1M).AgainstNegativeOrZero("decimalNegative"));
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-567M).AgainstNegativeOrZero("decimalNegative"));
        }

        [Fact]
        public void ThrowsGivenNegativeFloatValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1F).AgainstNegativeOrZero("floatNegative"));
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-4567F).AgainstNegativeOrZero("floatNegative"));
        }

        [Fact]
        public void ThrowsGivenNegativeDoubleValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-1.0).AgainstNegativeOrZero("doubleNegative"));
            Assert.Throws<ArgumentException>(() => Guard.WithValue(-456.453).AgainstNegativeOrZero("doubleNegative"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenPositiveValue()
        {
            Assert.Equal(1, Guard.WithValue(1).AgainstNegativeOrZero("intPositive").Value);
            Assert.Equal(1L, Guard.WithValue(1L).AgainstNegativeOrZero("longPositive").Value);
            Assert.Equal(1.0M, Guard.WithValue(1.0M).AgainstNegativeOrZero("decimalPositive").Value);
            Assert.Equal(1.0f, Guard.WithValue(1.0F).AgainstNegativeOrZero("floatPositive").Value);
            Assert.Equal(1.0, Guard.WithValue(1.0).AgainstNegativeOrZero("doublePositive").Value);
        }
    }
}
