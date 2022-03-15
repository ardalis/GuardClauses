using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNullOrEmpty
    {
        [Fact]
        public void DoesNothingGivenNonEmptyStringValue()
        {
            Guard.Against.NullOrEmpty("a", "string");
            Guard.Against.NullOrEmpty("1", "aNumericString");
        }

        [Fact]
        public void DoesNothingGivenNonEmptyGuidValue()
        {
            Guard.Against.NullOrEmpty(Guid.NewGuid(), "guid");
        }

        [Fact]
        public void DoesNothingGivenNonEmptyEnumerable()
        {
            Guard.Against.NullOrEmpty(new[] { "foo", "bar" }, "stringArray");
            Guard.Against.NullOrEmpty(new[] { 1, 2 }, "intArray");
        }

        [Fact]
        public void ThrowsGivenNullString()
        {
            string? nullString = null;
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrEmpty(nullString, "nullString"));
        }

        [Fact]
        public void ThrowsGivenEmptyString()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty("", "emptyString"));
        }

        [Fact]
        public void ThrowsGivenNullGuid()
        {
            Guid? nullGuid = null;
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrEmpty(nullGuid, "nullGuid"));
        }

        [Fact]
        public void ThrowsGivenEmptyGuid()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty(Guid.Empty, "emptyGuid"));
        }

        [Fact]
        public void ThrowsGivenEmptyEnumerable()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty(Enumerable.Empty<string>(), "emptyStringEnumerable"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenValidValue()
        {
            Assert.Equal("a", Guard.Against.NullOrEmpty("a", "string"));
            Assert.Equal("1", Guard.Against.NullOrEmpty("1", "aNumericString"));

            var collection1 = new[] { "foo", "bar" };
            Assert.Equal(collection1, Guard.Against.NullOrEmpty(collection1, "stringArray"));

            var collection2 = new[] { 1, 2 };
            Assert.Equal(collection2, Guard.Against.NullOrEmpty(collection2, "intArray"));
        }

        [Theory]
        [InlineData(null, "Required input xyz was empty. (Parameter 'xyz')")]
        [InlineData("Value is empty", "Value is empty (Parameter 'xyz')")]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvidedGivenStringValue(string customMessage, string expectedMessage)
        {
            string xyz = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty(xyz, message: customMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Contains(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, "Required input xyz was empty. (Parameter 'xyz')")]
        [InlineData("Value is empty", "Value is empty (Parameter 'xyz')")]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvidedGivenGuidValue(string customMessage, string expectedMessage)
        {
            Guid xyz = Guid.Empty;

            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty(xyz, message: customMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Contains(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, "Required input xyz was empty. (Parameter 'xyz')")]
        [InlineData("Value is empty", "Value is empty (Parameter 'xyz')")]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvidedGivenIEnumerableValue(string customMessage, string expectedMessage)
        {
            IEnumerable<string> xyz = Enumerable.Empty<string>();

            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty(xyz, message: customMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Contains(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, "Required input parameterName was empty. (Parameter 'parameterName')")]
        [InlineData("Value is empty", "Value is empty (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpectedWhenInputIsEmpty(string customMessage, string expectedMessage)
        {
            string emptyString = string.Empty;
            Guid emptyGuid = Guid.Empty;
            IEnumerable<string> emptyEnumerable = Enumerable.Empty<string>();

            var clausesToEvaluate = new List<Action>
            {
                () => Guard.Against.NullOrEmpty(emptyString, "parameterName", customMessage),
                () => Guard.Against.NullOrEmpty(emptyGuid, "parameterName", customMessage),
                () => Guard.Against.NullOrEmpty(emptyEnumerable, "parameterName", customMessage)
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
        [InlineData(null, "Value cannot be null. (Parameter 'parameterName')")]
        [InlineData("Value must be correct", "Value must be correct (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpectedWhenInputIsNull(string customMessage, string expectedMessage)
        {
            string? nullString = null;
            Guid? nullGuid = null;
            IEnumerable<string>? nullEnumerable = null;

            var clausesToEvaluate = new List<Action>
            {
                () => Guard.Against.NullOrEmpty(nullString, "parameterName", customMessage),
                () => Guard.Against.NullOrEmpty(nullGuid, "parameterName", customMessage),
                () => Guard.Against.NullOrEmpty(nullEnumerable, "parameterName", customMessage)
            };

            foreach (var clauseToEvaluate in clausesToEvaluate)
            {
                var exception = Assert.Throws<ArgumentNullException>(clauseToEvaluate);
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
        public void ExceptionParamNameMatchesExpectedWhenInputIsEmpty(string expectedParamName, string customMessage)
        {
            string emptyString = string.Empty;
            Guid emptyGuid = Guid.Empty;
            IEnumerable<string> emptyEnumerable = Enumerable.Empty<string>();

            var clausesToEvaluate = new List<Action>
            {
                () => Guard.Against.NullOrEmpty(emptyString, expectedParamName, customMessage),
                () => Guard.Against.NullOrEmpty(emptyGuid, expectedParamName, customMessage),
                () => Guard.Against.NullOrEmpty(emptyEnumerable, expectedParamName, customMessage)
            };

            foreach (var clauseToEvaluate in clausesToEvaluate)
            {
                var exception = Assert.Throws<ArgumentException>(clauseToEvaluate);
                Assert.NotNull(exception);
                Assert.Equal(expectedParamName, exception.ParamName);
            }
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "Please provide correct value")]
        [InlineData("SomeParameter", null)]
        [InlineData("SomeOtherParameter", "Value must be correct")]
        public void ExceptionParamNameMatchesExpectedWhenInputIsNull(string expectedParamName, string customMessage)
        {
            string? nullString = null;
            Guid? nullGuid = null;
            IEnumerable<string>? nullEnumerable = null;

            var clausesToEvaluate = new List<Action>
            {
                () => Guard.Against.NullOrEmpty(nullString, expectedParamName, customMessage),
                () => Guard.Against.NullOrEmpty(nullGuid, expectedParamName, customMessage),
                () => Guard.Against.NullOrEmpty(nullEnumerable, expectedParamName, customMessage)
            };

            foreach (var clauseToEvaluate in clausesToEvaluate)
            {
                var exception = Assert.Throws<ArgumentNullException>(clauseToEvaluate);
                Assert.NotNull(exception);
                Assert.Equal(expectedParamName, exception.ParamName);
            }
        }
    }
}
