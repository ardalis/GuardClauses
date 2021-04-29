﻿using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNotFound
    {
        [Fact]
        public void DoesNothingGivenNonNullValue()
        {
            Guard.Against.NotFound("mykey", "", "string");
            Guard.Against.NotFound(1, 1, "int");
            Guard.Against.NotFound(1, Guid.Empty, "guid");
            Guard.Against.NotFound(Guid.Empty, DateTime.Now, "datetime");
            Guard.Against.NotFound(1, new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            object obj = null!;
            Assert.Throws<NotFoundException>(() => Guard.Against.NotFound(1, obj, "null"));
        }

        [Fact]
        public void ReturnsExpectedValueWhenGivenNonNullValue()
        {
            Assert.Equal("", Guard.Against.NotFound("mykey", "", "string"));
            Assert.Equal(1, Guard.Against.NotFound(1, 1, "int"));

            var guid = Guid.Empty;
            Assert.Equal(guid, Guard.Against.NotFound(1, guid, "guid"));
            
            var now = DateTime.Now;
            Assert.Equal(now, Guard.Against.NotFound(1, now, "datetime"));

            var obj = new Object();
            Assert.Equal(obj, Guard.Against.NotFound(1, obj, "object"));
        }
    }
}
