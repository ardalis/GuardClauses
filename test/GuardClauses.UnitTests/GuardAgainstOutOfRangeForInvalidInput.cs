using Ardalis.GuardClauses;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForInvalidInput
    {
        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void DoesNothingGivenInRangeValue<T>(T input, Func<T, bool> func)
        {
            Guard.Against.InvalidInput(input, nameof(input), func);
        }

        [Theory]
        [ClassData(typeof(IncorrectClassData))]
        public void ThrowsGivenOutOfRangeValue<T>(T input, Func<T, bool> func)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.InvalidInput(input, nameof(input), func));
        }

        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void ReturnsExpectedValueGivenInRangeValue<T>(T input, Func<T, bool> func)
        {
            var result = Guard.Against.InvalidInput(input, nameof(input), func);
            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData(null, "Input parameterName did not satisfy the options")]
        [InlineData("Evaluation failed", "Evaluation failed")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.InvalidInput(10, "parameterName", x => x > 20, customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage + " (Parameter 'parameterName')", exception.Message);
        }

        // TODO: Test decimal types outside of ClassData
        // See: https://github.com/xunit/xunit/issues/2298
        public class CorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 20, (Func<int, bool>)((x) => x > 10) };
                yield return new object[] { DateAndTime.Now, (Func<DateTime, bool>)((x) => x > DateTime.MinValue) };
                yield return new object[] { 20.0f, (Func<float, bool>)((x) => x > 10.0f) };
                //yield return new object[] { 20.0m, (Func<decimal, bool>)((x) => x > 10.0m) };
                yield return new object[] { 20.0, (Func<double, bool>)((x) => x > 10.0) };
                yield return new object[] { long.MaxValue, (Func<long, bool>)((x) => x > 1) };
                yield return new object[] { short.MaxValue, (Func<short, bool>)((x) => x > 1) };
                yield return new object[] { "abcd", (Func<string, bool>)((x) => x == x.ToLower()) };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 20, (Func<int, bool>)((x) => x < 10) };
                yield return new object[] { DateAndTime.Now, (Func<DateTime, bool>)((x) => x > DateTime.MaxValue) };
                yield return new object[] { 20.0f, (Func<float, bool>)((x) => x > 30.0f) };
                //yield return new object[] { 20.0m, (Func<decimal, bool>)((x) => x > 30.0m) };
                yield return new object[] { 20.0, (Func<double, bool>)((x) => x > 30.0) };
                yield return new object[] { long.MaxValue, (Func<long, bool>)((x) => x < 1) };
                yield return new object[] { short.MaxValue, (Func<short, bool>)((x) => x < 1) };
                yield return new object[] { "abcd", (Func<string, bool>)((x) => x == x.ToUpper()) };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
