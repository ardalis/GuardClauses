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
            Guard.WithValue(-1).AgainstZero("minusOne");
            Guard.WithValue(1).AgainstZero("plusOne");
            Guard.WithValue(int.MinValue).AgainstZero("int.MinValue");
            Guard.WithValue(int.MaxValue).AgainstZero("int.MaxValue");
            Guard.WithValue(long.MinValue).AgainstZero("long.MinValue");
            Guard.WithValue(long.MaxValue).AgainstZero("long.MaxValue");
            Guard.WithValue(decimal.MinValue).AgainstZero("decimal.MinValue");
            Guard.WithValue(decimal.MaxValue).AgainstZero("decimal.MaxValue");
            Guard.WithValue(float.MinValue).AgainstZero("float.MinValue");
            Guard.WithValue(float.MaxValue).AgainstZero("float.MaxValue");
            Guard.WithValue(double.MinValue).AgainstZero("double.MinValue");
            Guard.WithValue(double.MaxValue).AgainstZero("double.MaxValue");
        }

        [Fact]
        public void ThrowsGivenZeroValueIntZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0).AgainstZero("zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueLongZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0L).AgainstZero("zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDecimalZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0M).AgainstZero("zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueFloatZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0F).AgainstZero("zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDoubleZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(0D).AgainstZero("zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDefaultInt()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(default(int)).AgainstZero("zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDefaultLong()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(default(long)).AgainstZero("zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDefaultDecimal()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(default(decimal)).AgainstZero("zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDefaultFloat()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(default(float)).AgainstZero( "zero"));
        }

        [Fact]
        public void ThrowsGivenZeroValueDefaultDouble()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(default(double)).AgainstZero("zero"));
        }


        [Fact]
        public void ThrowsGivenZeroValueDecimalDotZero()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(decimal.Zero).AgainstZero("zero"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenNonZeroValue()
        {
            Assert.Equal(-1, Guard.WithValue(-1).AgainstZero("minusOne").Value);
            Assert.Equal(1, Guard.WithValue(1).AgainstZero("plusOne").Value);
            Assert.Equal(int.MinValue, Guard.WithValue(int.MinValue).AgainstZero("int.MinValue").Value);
            Assert.Equal(int.MaxValue, Guard.WithValue(int.MaxValue).AgainstZero("int.MaxValue").Value);
            Assert.Equal(long.MinValue, Guard.WithValue(long.MinValue).AgainstZero("long.MinValue").Value);
            Assert.Equal(long.MaxValue, Guard.WithValue(long.MaxValue).AgainstZero("long.MaxValue").Value);
            Assert.Equal(decimal.MinValue, Guard.WithValue(decimal.MinValue).AgainstZero("decimal.MinValue").Value);
            Assert.Equal(decimal.MaxValue, Guard.WithValue(decimal.MaxValue).AgainstZero("decimal.MaxValue").Value);
            Assert.Equal(float.MinValue, Guard.WithValue(float.MinValue).AgainstZero("float.MinValue").Value);
            Assert.Equal(float.MaxValue, Guard.WithValue(float.MaxValue).AgainstZero("float.MaxValue").Value);
            Assert.Equal(double.MinValue, Guard.WithValue(double.MinValue).AgainstZero("double.MinValue").Value);
            Assert.Equal(double.MaxValue, Guard.WithValue(double.MaxValue).AgainstZero("double.MaxValue").Value);
        }
    }
}
