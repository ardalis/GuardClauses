using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForEnum
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void DoesNothingGivenInRangeValue(int enumValue)
        {
            Guard.Against.OutOfRange<TestEnum>(enumValue, nameof(enumValue));
        }
        

        [Theory]
        [InlineData(-1)]
        [InlineData(6)]
        [InlineData(10)]
        public void ThrowsGivenOutOfRangeValue(int enumValue)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange<TestEnum>(enumValue, nameof(enumValue)));
        }

        [Theory]
        [InlineData(TestEnum.Budgie)]
        [InlineData(TestEnum.Cat)]
        [InlineData(TestEnum.Dog)]
        [InlineData(TestEnum.Fish)]
        [InlineData(TestEnum.Frog)]
        [InlineData(TestEnum.Penguin)]
        public void DoesNothingGivenInRangeEnum(TestEnum enumValue)
        {
            Guard.Against.OutOfRange(enumValue, nameof(enumValue));
        }


        [Theory]
        [InlineData((TestEnum) (-1))]
        [InlineData((TestEnum) 6)]
        [InlineData((TestEnum) 10)]
        public void ThrowsGivenOutOfRangeEnum(TestEnum enumValue)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(enumValue, nameof(enumValue)));
        }
        
    }


    public enum TestEnum
    {
        Cat = 0,
        Dog = 1,
        Fish = 2,
        Budgie = 3,
        Penguin = 4,
        Frog = 5
    }
}
