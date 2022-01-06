using Ardalis.GuardClauses;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForEnumerableFloat
    {
        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void DoesNothingGivenInRangeValue(IEnumerable<float> input, float rangeFrom, float rangeTo)
        {
            Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo);
        }

        [Theory]
        [ClassData(typeof(IncorrectClassData))]
        public void ThrowsGivenOutOfRangeValue(IEnumerable<float> input, float rangeFrom, float rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo));
        }

        [Theory]
        [ClassData(typeof(IncorrectRangeClassData))]
        public void ThrowsGivenInvalidArgumentValue(IEnumerable<float> input, float rangeFrom, float rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo));
        }

        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void ReturnsExpectedValueGivenInRangeValue(IEnumerable<float> input, float rangeFrom, float rangeTo)
        {
            var result = Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo);
            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData(null, "rangeFrom should be less or equal than rangeTo (Parameter 'parameterName')")]
        [InlineData("Timespan range", "Timespan range (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpectedWhenRangeIsInvalid(string customMessage, string expectedMessage)
        {
            var input = new[] { 0f, 1f, 99f };
            float rangeFrom = 2f;
            float rangeTo = 1f;

            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, "parameterName", rangeFrom, rangeTo, customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "Please provide correct value")]
        [InlineData("SomeParameter", null)]
        [InlineData("SomeOtherParameter", "Value must be correct")]
        public void ExceptionParamNameMatchesExpectedRangeIsInvalid(string expectedParamName, string customMessage)
        {
            var input = new[] { 0f, 1f, 99f };
            float rangeFrom = 2f;
            float rangeTo = 1f;

            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, expectedParamName, rangeFrom, rangeTo, customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }

        [Theory]
        [InlineData(null, "Input parameterName had out of range item(s) (Parameter 'parameterName')")]
        [InlineData("Timespan range", "Timespan range (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpectedWhenInputIsInvalid(string customMessage, string expectedMessage)
        {
            var input = new[] { 0f, 1f, 99f };
            float rangeFrom = 0f;
            float rangeTo = 1f;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, "parameterName", rangeFrom, rangeTo, customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "Please provide correct value")]
        [InlineData("SomeParameter", null)]
        [InlineData("SomeOtherParameter", "Value must be correct")]
        public void ExceptionParamNameMatchesExpectedInputIsInvalid(string expectedParamName, string customMessage)
        {
            var input = new[] { 0f, 1f, 99f };
            float rangeFrom = 0f;
            float rangeTo = 1f;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, expectedParamName, rangeFrom, rangeTo, customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }

        public class CorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {new List<float> {float.MaxValue, 1.0f, float.MinValue}, float.MinValue, float.MaxValue};
                yield return new object[] {new List<float> {1100000.0f, 2000.8f, 120.5f, 180000.0f}, 100.0,1200000.0};
                yield return new object[] {new List<float> {0.1f, 128.0f, 200.5f}, 0.1, 200.5};
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<float> { 10.0f, 12.0f, 1500.0f }, 10.0, 1200.0 };
                yield return new object[] { new List<float> { 1000.0f, 200.0f, 120.0f, 180000.0f }, 100.0, 150000.0 };
                yield return new object[] { new List<float> { 15.0f, 120.0f, 158.0f }, 10.1, 110.0 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectRangeClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<float> { 10000.0f, 1200.0f, 15.0f }, 10000.0, 10.0 };
                yield return new object[] { new List<float> { 100010.0f, 200.0f, 120000.0f, 180.0f }, 2000000.0, 150.0 };
                yield return new object[] { new List<float> { 52000.0f, 86.0f, 2500000.0f }, 20000000.0, 100.0 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
