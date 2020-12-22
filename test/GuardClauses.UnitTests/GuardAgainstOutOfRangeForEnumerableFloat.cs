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
