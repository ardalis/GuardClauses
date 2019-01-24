using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstZero
    {
        [Fact]
        public void DoesNothingGivenNonZeroValue()
        {
            Guard.Against.Zero(-1, "minusOne");
            Guard.Against.Zero(1, "plusOne");
            Guard.Against.Zero(int.MinValue, "int.MinValue");
            Guard.Against.Zero(int.MaxValue, "int.MaxValue");
            Guard.Against.Zero(long.MinValue, "long.MinValue");
            Guard.Against.Zero(long.MaxValue, "long.MaxValue");
            Guard.Against.Zero(decimal.MinValue, "decimal.MinValue");
            Guard.Against.Zero(decimal.MaxValue, "decimal.MaxValue");
            Guard.Against.Zero(float.MinValue, "float.MinValue");
            Guard.Against.Zero(float.MaxValue, "float.MaxValue");
            Guard.Against.Zero(double.MinValue, "double.MinValue");
            Guard.Against.Zero(double.MaxValue, "double.MaxValue");
        }

        [Fact]
        public void ThrowsGivenZeroValueIntZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(0, "zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueLongZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(0L, "zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDecimalZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(0.0M, "zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueFloatZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(0.0f, "zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDoubleZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(0.0, "zero"));
        }



        [Fact]
        public void ThrowsGivenZeroValueDefaultInt()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(default(int), "zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDefaultLong()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(default(long), "zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDefaultDecimal()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(default(decimal), "zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDefaultFloat()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(default(float), "zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDefaultDouble()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(default(double), "zero"));
        }


        [Fact]
        public void ThrowsGivenZeroValueDecimalDotZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Zero(decimal.Zero, "zero"));
        }

    }
}
