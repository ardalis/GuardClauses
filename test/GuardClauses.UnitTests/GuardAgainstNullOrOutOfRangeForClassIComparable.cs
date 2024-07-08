using System;
using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    /// <summary>
    /// Every type that implements IComparable and IComparable<T> can use OutOfRange.
    /// Here for example tuples are used.
    /// </summary>
    public class GuardAgainstNullOrOutOfRangeForClassIComparable
    {
        private class TestObj : IComparable<TestObj?>, IEquatable<TestObj?>
        {
            private readonly int _internalValue;

            public TestObj(int internalValue)
            {
                _internalValue = internalValue;
            }

            public int CompareTo([AllowNull] TestObj? other) => _internalValue.CompareTo(other?._internalValue);


            public override bool Equals(object? obj)
            {
                if (obj is null) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(TestObj)) return false;
                return Equals((TestObj)obj);
            }
            public bool Equals([AllowNull] TestObj? other) => ReferenceEquals(this, other) || _internalValue == other?._internalValue;

            public override int GetHashCode() => _internalValue.GetHashCode();
        }

        [Fact]
        public void DoesNothingGivenInRangeValue()
        {
            Guard.Against.NullOrOutOfRange(new TestObj(1), "index", new TestObj(1), new TestObj(1));
            Guard.Against.NullOrOutOfRange(new TestObj(2), "index", new TestObj(1), new TestObj(3));
            Guard.Against.NullOrOutOfRange(new TestObj(3), "index", new TestObj(1), new TestObj(3));
        }
        [Fact]
        public void ThrowsGivenOutOfRangeValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.NullOrOutOfRange(new TestObj(-1), "index", new TestObj(1), new TestObj(3)));
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.NullOrOutOfRange(new TestObj(0), "index", new TestObj(1), new TestObj(3)));
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.NullOrOutOfRange(new TestObj(4), "index", new TestObj(1), new TestObj(3)));
        }

        [Fact]
        public void ThrowsCustomExceptionWhenSuppliedGivenOutOfRangeValue()
        {
            Exception customException = new Exception();
            Assert.Throws<Exception>(() => Guard.Against.NullOrOutOfRange(new TestObj(-1), "index", new TestObj(1), new TestObj(3), exceptionCreator: () => customException));
            Assert.Throws<Exception>(() => Guard.Against.NullOrOutOfRange(new TestObj(0), "index", new TestObj(1), new TestObj(3), exceptionCreator: () => customException));
            Assert.Throws<Exception>(() => Guard.Against.NullOrOutOfRange(new TestObj(4), "index", new TestObj(1), new TestObj(3), exceptionCreator: () => customException));
        }

        [Fact]
        public void ThrowsGivenInvalidArgumentValue()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrOutOfRange(new TestObj(-1), "index", new TestObj(3), new TestObj(1)));
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrOutOfRange(new TestObj(0), "index", new TestObj(3), new TestObj(1)));
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrOutOfRange(new TestObj(4), "index", new TestObj(3), new TestObj(1)));
        }

        [Fact]
        public void ThrowsGivenInvalidNullArgumentValue()
        {
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrOutOfRange(null, "index", new TestObj(3), new TestObj(1)));
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrOutOfRange(new TestObj(0), "index", null, new TestObj(1)));
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrOutOfRange(new TestObj(4), "index", new TestObj(3), null));
#pragma warning restore CS8631
        }

        [Fact]
        public void ReturnsExpectedValueGivenInRangeValue()
        {
            Assert.Equal(new TestObj(1), Guard.Against.NullOrOutOfRange(new TestObj(1), "index", new TestObj(1), new TestObj(1)));
            Assert.Equal(new TestObj(1), Guard.Against.NullOrOutOfRange(new TestObj(1), "index", new TestObj(1), new TestObj(3)));
            Assert.Equal(new TestObj(2), Guard.Against.NullOrOutOfRange(new TestObj(2), "index", new TestObj(1), new TestObj(3)));
            Assert.Equal(new TestObj(3), Guard.Against.NullOrOutOfRange(new TestObj(3), "index", new TestObj(1), new TestObj(3)));
        }
    }
}
