using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfSQLDateRange
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

            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.WithValue(date).AgainstOutOfSQLDateRange(nameof(date)));
        }

        [Fact]
        public void DoNothingGivenCurrentDate()
        {
            Guard.WithValue(DateTime.Today).AgainstOutOfSQLDateRange("Today");
            Guard.WithValue(DateTime.Now).AgainstOutOfSQLDateRange("Now");
            Guard.WithValue(DateTime.UtcNow).AgainstOutOfSQLDateRange("UTC Now");
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

            Guard.WithValue(date).AgainstOutOfSQLDateRange(nameof(date));
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

            Guard.WithValue(date).AgainstOutOfSQLDateRange(nameof(date));
        }

        [Theory]
        [MemberData(nameof(GetSqlDateTimeTestVectors))]
        public void ReturnsExpectedValueWhenGivenValidSqlDateTime(DateTime input, string name, DateTime expected)
        {
            Assert.Equal(expected, Guard.WithValue(input).AgainstOutOfSQLDateRange(name).Value);
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
