﻿using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNullOrOutOfSQLDateRange
    {
        [Theory]
        [InlineData(1)]
        [InlineData(60)]
        [InlineData(60 * 60)]
        [InlineData(60 * 60 * 24)]
        [InlineData(60 * 60 * 24 * 30)]
        [InlineData(60 * 60 * 24 * 30 * 365)]
        public void ThrowsGivenValueBelowMinDate(int offsetInSeconds)
        {
            DateTime date = SqlDateTime.MinValue.Value.AddSeconds(-offsetInSeconds);

            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.NullOrOutOfSQLDateRange(date, nameof(date)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(60)]
        [InlineData(60 * 60)]
        [InlineData(60 * 60 * 24)]
        [InlineData(60 * 60 * 24 * 30)]
        [InlineData(60 * 60 * 24 * 30 * 365)]
        public void ThrowsCustomExceptionWhenSuppliedGivenValueBelowMinDate(int offsetInSeconds)
        {
            DateTime date = SqlDateTime.MinValue.Value.AddSeconds(-offsetInSeconds);
            Exception customException = new Exception();

            Assert.Throws<Exception>(() => Guard.Against.NullOrOutOfSQLDateRange(date, nameof(date), exceptionCreator: () => customException));
        }


        [Fact]
        public void DoNothingGivenCurrentDate()
        {
            Guard.Against.NullOrOutOfSQLDateRange(DateTime.Today, "Today");
            Guard.Against.NullOrOutOfSQLDateRange(DateTime.Now, "Now");
            Guard.Against.NullOrOutOfSQLDateRange(DateTime.UtcNow, "UTC Now");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(60)]
        [InlineData(60 * 60)]
        [InlineData(60 * 60 * 24)]
        [InlineData(60 * 60 * 24 * 30)]
        [InlineData(60 * 60 * 24 * 30 * 365)]
        public void DoNothingGivenSqlMinValue(int offsetInSeconds)
        {
            DateTime date = SqlDateTime.MinValue.Value.AddSeconds(offsetInSeconds);

            Guard.Against.NullOrOutOfSQLDateRange(date, nameof(date));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(60)]
        [InlineData(60 * 60)]
        [InlineData(60 * 60 * 24)]
        [InlineData(60 * 60 * 24 * 30)]
        [InlineData(60 * 60 * 24 * 30 * 365)]
        public void DoNothingGivenSqlMaxValue(int offsetInSeconds)
        {
            DateTime date = SqlDateTime.MaxValue.Value.AddSeconds(-offsetInSeconds);

            Guard.Against.NullOrOutOfSQLDateRange(date, nameof(date));
        }

        [Theory]
        [MemberData(nameof(GetSqlDateTimeTestVectors))]
        public void ReturnsExpectedValueWhenGivenValidSqlDateTime(DateTime input, string name, DateTime expected)
        {
            Assert.Equal(expected, Guard.Against.NullOrOutOfSQLDateRange(input, name));
        }

        [Theory]
        [InlineData(null, "Input date was out of range (Parameter 'date')")]
        [InlineData("SQLDate range", "SQLDate range (Parameter 'date')")]
        public void ErrorMessageMatchesExpected(string? customMessage, string expectedMessage)
        {
            DateTime date = SqlDateTime.MinValue.Value.AddSeconds(-1);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.NullOrOutOfSQLDateRange(date, nameof(date), customMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void ThrowsGivenInvalidNullArgumentValue()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrOutOfSQLDateRange(null, "index"));
        }

        [Fact]
        public void ThrowsCustomExceptionWhenSuppliedGivenInvalidNullArgumentValue()
        {
            Exception customException = new Exception();
            Assert.Throws<Exception>(() => Guard.Against.NullOrOutOfSQLDateRange(null, "index", exceptionCreator: () => customException));
        }
        public static IEnumerable<object[]> GetSqlDateTimeTestVectors()
        {
            var now = DateTime.Now;
            var utc = DateTime.UtcNow;
            var yesterday = DateTime.Now.AddDays(-1);
            var min = SqlDateTime.MinValue.Value;
            var max = SqlDateTime.MaxValue.Value;

            yield return new object[] {now, "now", now};
            yield return new object[] {utc, "utc", utc};
            yield return new object[] {yesterday, "yesterday", yesterday};
            yield return new object[] {min, "min", min};
            yield return new object[] {max, "max", max};
        }
    }
}
