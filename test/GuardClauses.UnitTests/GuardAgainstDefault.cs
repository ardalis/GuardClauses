using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstDefault
    {
        [Fact]
        public void DoesNothingGivenNonDefaultValue()
        {
            Guard.Against.Default("", "string");
            Guard.Against.Default(1, "int");
            Guard.Against.Default(Guid.NewGuid(), "guid");
            Guard.Against.Default(DateTime.Now, "datetime");
            Guard.Against.Default(new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenDefaultValue()
        {
            Assert.Throws<ArgumentException>("string", () => Guard.Against.Default(default(string), "string"));
            Assert.Throws<ArgumentException>("int", () => Guard.Against.Default(default(int), "int"));
            Assert.Throws<ArgumentException>("guid", () => Guard.Against.Default(default(Guid), "guid"));
            Assert.Throws<ArgumentException>("datetime", () => Guard.Against.Default(default(DateTime), "datetime"));
            Assert.Throws<ArgumentException>("object", () => Guard.Against.Default(default(object), "object"));
        }

        [Theory]
        [MemberData(nameof(GetNonDefaultTestVectors))]
        public void ReturnsExpectedValueWhenGivenNonDefaultValue(object input, string name, object expected)
        {
            var actual = Guard.Against.Default(input, name);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, "Parameter [parameterName] is default value for type String (Parameter 'parameterName')")]
        [InlineData("Please provide correct value", "Please provide correct value (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Default(default(string), "parameterName", customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, "Parameter [xyz] is default value for type String (Parameter 'xyz')")]
        [InlineData("Please provide correct value", "Please provide correct value (Parameter 'xyz')")]
        public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvided(string customMessage, string expectedMessage)
        {
            var xyz = default(string);
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Default(xyz, message: customMessage));
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
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Default(default(string), expectedParamName, customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }

        public static IEnumerable<object[]> GetNonDefaultTestVectors()
        {
            yield return new object[] { "", "string", "" };
            yield return new object[] { 1, "int", 1 };

            var guid = Guid.NewGuid();
            yield return new object[] { guid, "guid", guid };

            var now = DateTime.Now;
            yield return new object[] { now, "now", now };

            var obj = new Object();
            yield return new object[] { obj, "obj", obj };
        }
    }
}
