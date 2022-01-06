using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    /// <summary>
    /// Every type that implements IComparable and IComparable<T> can use OutOfRange.
    /// Here for example tuples are used.
    /// </summary>
    public class GuardAgainstOutOfRangeForIComparable
    {
        [Theory]
        [InlineData(1, 1, 1, 1, 10, 10)]
        [InlineData(5, 5, 1, 1, 10, 10)]
        [InlineData(10, 10, 1, 1, 10, 10)]
        public void DoesNothingGivenInRangeValue(int input1, int input2, int rangeFrom1, int rangeFrom2, int rangeTo1, int rangeTo2)
        {
            Guard.Against.OutOfRange((input1, input2), "tuple", (rangeFrom1, rangeFrom2), (rangeTo1, rangeTo2));
        }

        [Theory]
        [InlineData(0, 1, 1, 1, 10, 10)]
        [InlineData(1, 0, 1, 1, 10, 10)]
        [InlineData(10, 11, 1, 1, 10, 10)]
        [InlineData(11, 10, 1, 1, 10, 10)]
        public void ThrowsGivenOutOfRangeValue(int input1, int input2, int rangeFrom1, int rangeFrom2, int rangeTo1, int rangeTo2)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange((input1, input2), "tuple", (rangeFrom1, rangeFrom2), (rangeTo1, rangeTo2)));
        }

        [Theory]
        [InlineData(0, 1, 10, 10, 1, 1)]
        [InlineData(1, 0, 10, 10, 1, 1)]
        [InlineData(10, 11, 10, 10, 1, 1)]
        [InlineData(11, 10, 10, 10, 1, 1)]
        public void ThrowsGivenInvalidArgumentValue(int input1, int input2, int rangeFrom1, int rangeFrom2, int rangeTo1, int rangeTo2)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange((input1, input2), "tuple", (rangeFrom1, rangeFrom2), (rangeTo1, rangeTo2)));
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 10, 10)]
        [InlineData(5, 5, 1, 1, 10, 10)]
        [InlineData(10, 10, 1, 1, 10, 10)]
        public void ReturnsExpectedValueGivenInRangeValue(int input1, int input2, int rangeFrom1, int rangeFrom2, int rangeTo1, int rangeTo2)
        {
            var input = (input1, input2);
            var actual = Guard.Against.OutOfRange(input, "tuple", (rangeFrom1, rangeFrom2), (rangeTo1, rangeTo2));
            Assert.Equal(input, actual);
        }

        [Theory]
        [InlineData(null, "Input parameterName was out of range (Parameter 'parameterName')")]
        [InlineData("Tuple range", "Tuple range (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange((1,2), "parameterName", (3,3), (9,9), customMessage));
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
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange((1, 2), expectedParamName, (3, 3), (9, 9), customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }
    }
}
