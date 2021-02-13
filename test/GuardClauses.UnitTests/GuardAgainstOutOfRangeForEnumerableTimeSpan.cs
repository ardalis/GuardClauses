﻿using Ardalis.GuardClauses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForEnumerableTimeSpan
    {
        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void DoesNothingGivenInRangeValue(IEnumerable<int> input, int rangeFrom, int rangeTo)
        {
            var inputTimeSpan = input.Select(i => TimeSpan.FromSeconds(i));
            var rangeFromTimeSpan = TimeSpan.FromSeconds(rangeFrom);
            var rangeToTimeSpan = TimeSpan.FromSeconds(rangeTo);

            Guard.Against.OutOfRange(inputTimeSpan, nameof(inputTimeSpan), rangeFromTimeSpan, rangeToTimeSpan);
        }

        [Theory]
        [ClassData(typeof(IncorrectClassData))]
        public void ThrowsGivenOutOfRangeValue(IEnumerable<int> input, int rangeFrom, int rangeTo)
        {
            var inputTimeSpan = input.Select(i => TimeSpan.FromSeconds(i));
            var rangeFromTimeSpan = TimeSpan.FromSeconds(rangeFrom);
            var rangeToTimeSpan = TimeSpan.FromSeconds(rangeTo);

            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(inputTimeSpan, nameof(inputTimeSpan), rangeFromTimeSpan, rangeToTimeSpan));
        }

        [Theory]
        [ClassData(typeof(IncorrectRangeClassData))]
        public void ThrowsGivenInvalidArgumentValue(IEnumerable<int> input, int rangeFrom, int rangeTo)
        {
            var inputTimeSpan = input.Select(i => TimeSpan.FromSeconds(i));
            var rangeFromTimeSpan = TimeSpan.FromSeconds(rangeFrom);
            var rangeToTimeSpan = TimeSpan.FromSeconds(rangeTo);

            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(inputTimeSpan, nameof(inputTimeSpan), rangeFromTimeSpan, rangeToTimeSpan));
        }

        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void ReturnsExpectedValueGivenInRangeValue(IEnumerable<int> input, int rangeFrom, int rangeTo)
        {
            var inputTimeSpan = input.Select(i => TimeSpan.FromSeconds(i));
            var rangeFromTimeSpan = TimeSpan.FromSeconds(rangeFrom);
            var rangeToTimeSpan = TimeSpan.FromSeconds(rangeTo);

            var result = Guard.Against.OutOfRange(inputTimeSpan, nameof(inputTimeSpan), rangeFromTimeSpan, rangeToTimeSpan);
            Assert.Equal(inputTimeSpan, result);
        }


        public class CorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<int> { 1000, 1200, 1500 }, 1000, 2000 };
                yield return new object[] { new List<int> { 100000, 2000, 120, 180000 }, 100, 200000 };
                yield return new object[] { new List<int> { 18, 128, 108 }, 0, 200 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<int> { 10, 12, 1500 }, 10, 1200 };
                yield return new object[] { new List<int> { 1000, 200, 120, 180000 }, 100, 150000 };
                yield return new object[] { new List<int> { 15, 120, 158 }, 10, 110 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectRangeClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<int> { 10000, 1200, 15 }, 10000, 10 };
                yield return new object[] { new List<int> { 100010, 200, 120000, 180 }, 2000000, 150 };
                yield return new object[] { new List<int> { 52000, 86, 2500000 }, 20000000, 100 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
