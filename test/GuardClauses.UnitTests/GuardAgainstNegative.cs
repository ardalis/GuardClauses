using System;
using System.Collections.Generic;
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

        [Theory]
        [InlineData(null, "Required input parameterName cannot be negative. (Parameter 'parameterName')")]
        [InlineData("Must be positive", "Must be positive (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var clausesToEvaluate = new List<Action>
            {
                () => Guard.Against.Negative(-1, "parameterName", customMessage),
                () => Guard.Against.Negative(-1L, "parameterName", customMessage),
                () => Guard.Against.Negative(-1.0M, "parameterName", customMessage),
                () => Guard.Against.Negative(-1.0f, "parameterName", customMessage),
                () => Guard.Against.Negative(-1.0, "parameterName", customMessage),
                () => Guard.Against.Negative(TimeSpan.FromSeconds(-1), "parameterName", customMessage)
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
        [InlineData(null, null)]
        [InlineData(null, "Please provide correct value")]
        [InlineData("SomeParameter", null)]
        [InlineData("SomeOtherParameter", "Value must be correct")]
        public void ExceptionParamNameMatchesExpected(string expectedParamName, string customMessage)
        {
            var clausesToEvaluate = new List<Action>
            {
                () => Guard.Against.Negative(-1, expectedParamName, customMessage),
                () => Guard.Against.Negative(-1L, expectedParamName, customMessage),
                () => Guard.Against.Negative(-1.0M, expectedParamName, customMessage),
                () => Guard.Against.Negative(-1.0f, expectedParamName, customMessage),
                () => Guard.Against.Negative(-1.0, expectedParamName, customMessage),
                () => Guard.Against.Negative(TimeSpan.FromSeconds(-1), expectedParamName, customMessage)
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
