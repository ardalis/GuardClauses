using Ardalis.GuardClauses;
using System;
using System.Data.SqlTypes;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstInvalidSqlDate
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

            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.InvalidSqlDate(date, nameof(date)));
        }

        [Fact]
        public void DoNothingGivenCurrentDate()
        {
            Guard.Against.InvalidSqlDate(DateTime.Today, "Today");
            Guard.Against.InvalidSqlDate(DateTime.Now, "Now");
            Guard.Against.InvalidSqlDate(DateTime.UtcNow, "UTC Now");
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

            Guard.Against.InvalidSqlDate(date, nameof(date));
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

            Guard.Against.InvalidSqlDate(date, nameof(date));
        }
    }
}
