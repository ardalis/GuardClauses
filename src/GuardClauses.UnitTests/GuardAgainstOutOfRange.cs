﻿using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRange
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(3, 1, 3)]
        public void DoesNothingGivenInRangeValueUsingShortcutMethod(int input, int rangeFrom, int rangeTo)
        {
            Guard.AgainstOutOfRange(input, "index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(3, 1, 3)]
        public void DoesNothingGivenInRangeValueUsingExtensionMethod(int input, int rangeFrom, int rangeTo)
        {
            Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo);
        }

        [Theory]
        [InlineData(-1, 1, 3)]
        [InlineData(0, 1, 3)]
        [InlineData(4, 1, 3)]
        public void ThrowsGivenOutOfRangeValueUsingShortcutMethod(int input, int rangeFrom, int rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.AgainstOutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(-1, 1, 3)]
        [InlineData(0, 1, 3)]
        [InlineData(4, 1, 3)]
        public void ThrowsGivenOutOfRangeValueUsingExtensionMethod(int input, int rangeFrom, int rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(-1, 3, 1)]
        [InlineData(0, 3, 1)]
        [InlineData(4, 3, 1)]
        public void ThrowsGivenInvalidArgumentValueUsingShortcutMethod(int input, int rangeFrom, int rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }

        [Theory]
        [InlineData(-1, 3, 1)]
        [InlineData(0, 3, 1)]
        [InlineData(4, 3, 1)]
        public void ThrowsGivenInvalidArgumentValueUsingExtensionMethod(int input, int rangeFrom, int rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, "index", rangeFrom, rangeTo));
        }
    }
}