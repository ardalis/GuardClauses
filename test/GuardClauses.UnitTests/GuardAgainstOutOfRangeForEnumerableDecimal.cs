using Ardalis.GuardClauses;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForEnumerableDecimal
    {
        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void DoesNothingGivenInRangeValue(IEnumerable<decimal> input, decimal rangeFrom, decimal rangeTo)
        {
            Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo);
        }

        [Theory]
        [ClassData(typeof(IncorrectClassData))]
        public void ThrowsGivenOutOfRangeValue(IEnumerable<decimal> input, decimal rangeFrom, decimal rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo));
        }

        [Theory]
        [ClassData(typeof(IncorrectRangeClassData))]
        public void ThrowsGivenInvalidArgumentValue(IEnumerable<decimal> input, decimal rangeFrom, decimal rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo));
        }

        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void ReturnsExpectedValueGivenInRangeValue(IEnumerable<decimal> input, decimal rangeFrom, decimal rangeTo)
        {
            var result = Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo);
            Assert.Equal(input, result);
        }


        public class CorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<decimal> { decimal.MaxValue, 1.0m, decimal.MinValue }, decimal.MinValue, decimal.MaxValue };
                yield return new object[] { new List<decimal> { 1100000.0m, 2000.8m, 120.5m, 180000.0m }, 100.0, 1200000.0 };
                yield return new object[] { new List<decimal> { 0.1m, 128, 200.5m }, 0.1, 200.5 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<decimal> { 10.0m, 12.0m, 1500.0m }, 10.0, 1200.0 };
                yield return new object[] { new List<decimal> { 1000.0m, 200.0m, 120.0m, 180000.0m }, 100.0, 150000.0 };
                yield return new object[] { new List<decimal> { 5.0m, 120.0m, 158.0m }, 10.1, 110.0 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectRangeClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<decimal> { 10000.0m, 1200.0m, 15.0m }, 10000.0, 10.0 };
                yield return new object[] { new List<decimal> { 100010.0m, 200.0m, 120000.0m, 0.2m }, 2000000.0, 150.0 };
                yield return new object[] { new List<decimal> { 52000.0m, 86.0m, 2500000.0m }, 20000000.0, 100.0 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
