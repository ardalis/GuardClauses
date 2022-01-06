using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNegativeOrZero
    {
        [Fact]
        public void DoesNothingGivenPositiveValue() 
        {
            Guard.Against.NegativeOrZero(1, "intPositive");
            Guard.Against.NegativeOrZero(1L, "longPositive");
            Guard.Against.NegativeOrZero(1.0M, "decimalPositive");
            Guard.Against.NegativeOrZero(1.0f, "floatPositive");
            Guard.Against.NegativeOrZero(1.0, "doublePositive");
            Guard.Against.NegativeOrZero(TimeSpan.FromSeconds(1), "timespanPositive");
        }

        [Fact]
        public void ThrowsGivenZeroIntValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(0, "intZero"));
        }

        [Fact]
        public void ThrowsGivenZeroLongValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(0L, "longZero"));
        }

        [Fact]
        public void ThrowsGivenZeroDecimalValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(0M, "decimalZero"));
        }

        [Fact]
        public void ThrowsGivenZeroFloatValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(0f, "floatZero"));
        }

        [Fact]
        public void ThrowsGivenZeroDoubleValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(0.0, "doubleZero"));
        }

        [Fact]
        public void ThrowsGivenZeroTimeSpanValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(TimeSpan.Zero, "timespanZero"));
        }


        [Fact]
        public void ThrowsGivenNegativeIntValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-1, "intNegative"));
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-42, "intNegative"));
        }

        [Fact]
        public void ThrowsGivenNegativeLongValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-1L, "longNegative"));
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-456L, "longNegative"));
        }

        [Fact]
        public void ThrowsGivenNegativeDecimalValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-1M, "decimalNegative"));
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-567M, "decimalNegative"));
        }

        [Fact]
        public void ThrowsGivenNegativeFloatValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-1f, "floatNegative"));
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-4567f, "floatNegative"));
        }

        [Fact]
        public void ThrowsGivenNegativeDoubleValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-1.0, "doubleNegative"));
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-456.453, "doubleNegative"));
        }

        [Fact]
        public void ThrowsGivenNegativeTimeSpanValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(TimeSpan.FromSeconds(-1), "timespanNegative"));
            Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(TimeSpan.FromSeconds(-456), "timespanNegative"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenPositiveValue()
        {
            Assert.Equal(1, Guard.Against.NegativeOrZero(1, "intPositive"));
            Assert.Equal(1L, Guard.Against.NegativeOrZero(1L, "longPositive"));
            Assert.Equal(1.0M, Guard.Against.NegativeOrZero(1.0M, "decimalPositive"));
            Assert.Equal(1.0f, Guard.Against.NegativeOrZero(1.0f, "floatPositive"));
            Assert.Equal(1.0, Guard.Against.NegativeOrZero(1.0, "doublePositive"));
            Assert.Equal(TimeSpan.FromSeconds(1), Guard.Against.NegativeOrZero(TimeSpan.FromSeconds(1), "timespanPositive"));
        }

        [Theory]
        [InlineData(-1, null, "Required input parameterName cannot be zero or negative. (Parameter 'parameterName')")]
        [InlineData(-1, "Must be positive", "Must be positive (Parameter 'parameterName')")]
        [InlineData(0, null, "Required input parameterName cannot be zero or negative. (Parameter 'parameterName')")]
        [InlineData(0, "Must be positive", "Must be positive (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpected(int input, string customMessage, string expectedMessage)
        {
            var clausesToEvaluate = new List<Action>
            {
                () => Guard.Against.NegativeOrZero(input, "parameterName", customMessage),
                () => Guard.Against.NegativeOrZero((long)input, "parameterName", customMessage),
                () => Guard.Against.NegativeOrZero((decimal)input, "parameterName", customMessage),
                () => Guard.Against.NegativeOrZero((float)input, "parameterName", customMessage),
                () => Guard.Against.NegativeOrZero((double)input, "parameterName", customMessage),
                () => Guard.Against.NegativeOrZero(TimeSpan.FromSeconds(input), "parameterName", customMessage)
            };

            foreach (var clauseToEvaluate in clausesToEvaluate)
            {
                var exception = Assert.Throws<ArgumentException>(clauseToEvaluate);
                Assert.NotNull(exception);
                Assert.NotNull(exception.Message);
                Assert.Equal(expectedMessage, exception.Message);
            }
        }

        [Theory]
        [InlineData(-1, null, null)]
        [InlineData(-1, null, "Please provide correct value")]
        [InlineData(-1, "SomeParameter", null)]
        [InlineData(-1, "SomeOtherParameter", "Value must be correct")]
        [InlineData(0, null, null)]
        [InlineData(0, null, "Please provide correct value")]
        [InlineData(0, "SomeParameter", null)]
        [InlineData(0, "SomeOtherParameter", "Value must be correct")]
        public void ExceptionParamNameMatchesExpected(int input, string expectedParamName, string customMessage)
        {
            var clausesToEvaluate = new List<Action>
            {
                () => Guard.Against.NegativeOrZero(input, expectedParamName, customMessage),
                () => Guard.Against.NegativeOrZero((long)input, expectedParamName, customMessage),
                () => Guard.Against.NegativeOrZero((decimal)input, expectedParamName, customMessage),
                () => Guard.Against.NegativeOrZero((float)input, expectedParamName, customMessage),
                () => Guard.Against.NegativeOrZero((double)input, expectedParamName, customMessage),
                () => Guard.Against.NegativeOrZero(TimeSpan.FromSeconds(input), expectedParamName, customMessage)
            };

            foreach (var clauseToEvaluate in clausesToEvaluate)
            {
                var exception = Assert.Throws<ArgumentException>(clauseToEvaluate);
                Assert.NotNull(exception);
                Assert.Equal(expectedParamName, exception.ParamName);
            }
        }
    }
}
