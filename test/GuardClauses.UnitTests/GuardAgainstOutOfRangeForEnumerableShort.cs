using Ardalis.GuardClauses;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForEnumerableShort
    {
        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void DoesNothingGivenInRangeValue(IEnumerable<short> input, short rangeFrom, short rangeTo)
        {
            Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo);
        }

        [Theory]
        [ClassData(typeof(IncorrectClassData))]
        public void ThrowsGivenOutOfRangeValue(IEnumerable<short> input, short rangeFrom, short rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo));
        }

        [Theory]
        [ClassData(typeof(IncorrectRangeClassData))]
        public void ThrowsGivenInvalidArgumentValue(IEnumerable<short> input, short rangeFrom, short rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo));
        }

        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void ReturnsExpectedValueGivenInRangeValue(IEnumerable<short> input, short rangeFrom, short rangeTo)
        {
            var result = Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo);
            Assert.Equal(input, result);
        }


        public class CorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<short> { 10, 12, 15 }, 10, 20 };
                yield return new object[] { new List<short> { 100, 200, 120, 180 }, 100, 200 };
                yield return new object[] { new List<short> { 18, 128, 108 }, 0, 200 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<short> { 10, 12, 15 }, 10, 12 };
                yield return new object[] { new List<short> { 100, 200, 120, 180 }, 100, 150 };
                yield return new object[] { new List<short> { 15, 120, 158 }, 10, 110 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectRangeClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<short> { 10, 12, 15 }, 10, 9 };
                yield return new object[] { new List<short> { 100, 200, 120, 180 }, 200, 150 };
                yield return new object[] { new List<short> { 52, 86, 250 }, 200, 100 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
