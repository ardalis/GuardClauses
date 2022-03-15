using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
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

        [Fact]
        public void ReturnsExpectedValueWhenGivenNonZeroValue()
        {
            Assert.Equal(-1, Guard.Against.Zero(-1, "minusOne"));
            Assert.Equal(1, Guard.Against.Zero(1, "plusOne"));
            Assert.Equal(int.MinValue, Guard.Against.Zero(int.MinValue, "int.MinValue"));
            Assert.Equal(int.MaxValue, Guard.Against.Zero(int.MaxValue, "int.MaxValue"));
            Assert.Equal(long.MinValue, Guard.Against.Zero(long.MinValue, "long.MinValue"));
            Assert.Equal(long.MaxValue, Guard.Against.Zero(long.MaxValue, "long.MaxValue"));
            Assert.Equal(decimal.MinValue, Guard.Against.Zero(decimal.MinValue, "decimal.MinValue"));
            Assert.Equal(decimal.MaxValue, Guard.Against.Zero(decimal.MaxValue, "decimal.MaxValue"));
            Assert.Equal(float.MinValue, Guard.Against.Zero(float.MinValue, "float.MinValue"));
            Assert.Equal(float.MaxValue, Guard.Against.Zero(float.MaxValue, "float.MaxValue"));
            Assert.Equal(double.MinValue, Guard.Against.Zero(double.MinValue, "double.MinValue"));
            Assert.Equal(double.MaxValue, Guard.Against.Zero(double.MaxValue, "double.MaxValue"));
        }

        [Theory]
        [InlineData(null, "Required input parameterName cannot be zero. (Parameter 'parameterName')")]
        [InlineData("Value is ZERO", "Value is ZERO (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var clausesToEvaluate = new List<Action>
            {
                () => Guard.Against.Zero(0, "parameterName", customMessage),
                () => Guard.Against.Zero(0L, "parameterName", customMessage),
                () => Guard.Against.Zero(0.0M, "parameterName", customMessage),
                () => Guard.Against.Zero(0.0f, "parameterName", customMessage),
                () => Guard.Against.Zero(0.0, "parameterName", customMessage)
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
        [InlineData(null, "Required input xyz cannot be zero. (Parameter 'xyz')")]
        [InlineData("Value is ZERO", "Value is ZERO (Parameter 'xyz')")]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvidedGivenIntValue(string customMessage, string expectedMessage)
        {
            var xyz = 0;
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Zero(xyz, message: customMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, "Required input xyz cannot be zero. (Parameter 'xyz')")]
        [InlineData("Value is ZERO", "Value is ZERO (Parameter 'xyz')")]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvidedGivenLongValue(string customMessage, string expectedMessage)
        {
            var xyz = 0L;
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Zero(xyz, message: customMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, "Required input xyz cannot be zero. (Parameter 'xyz')")]
        [InlineData("Value is ZERO", "Value is ZERO (Parameter 'xyz')")]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvidedGivenDecimalValue(string customMessage, string expectedMessage)
        {
            var xyz = 0.0M;
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Zero(xyz, message: customMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, "Required input xyz cannot be zero. (Parameter 'xyz')")]
        [InlineData("Value is ZERO", "Value is ZERO (Parameter 'xyz')")]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvidedGivenFloatValue(string customMessage, string expectedMessage)
        {
            var xyz = 0.0f;
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Zero(xyz, message: customMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, "Required input xyz cannot be zero. (Parameter 'xyz')")]
        [InlineData("Value is ZERO", "Value is ZERO (Parameter 'xyz')")]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvidedGivenDoubleValue(string customMessage, string expectedMessage)
        {
            var xyz = 0.0;
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Zero(xyz, message: customMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
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
                () => Guard.Against.Zero(0, expectedParamName, customMessage),
                () => Guard.Against.Zero(0L, expectedParamName, customMessage),
                () => Guard.Against.Zero(0.0M, expectedParamName, customMessage),
                () => Guard.Against.Zero(0.0f, expectedParamName, customMessage),
                () => Guard.Against.Zero(0.0, expectedParamName, customMessage)
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
