using Ardalis.GuardClauses;
using System.ComponentModel;
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
            var exception = Assert.Throws<InvalidEnumArgumentException>(() => Guard.Against.OutOfRange<TestEnum>(enumValue, nameof(enumValue)));
            Assert.Equal(nameof(enumValue), exception.ParamName);
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
            var exception = Assert.Throws<InvalidEnumArgumentException>(() => Guard.Against.OutOfRange(enumValue, nameof(enumValue)));
            Assert.Equal(nameof(enumValue), exception.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ReturnsExpectedValueGivenInRangeValue(int enumValue)
        {
            var expected = enumValue;
            Assert.Equal(expected, Guard.Against.OutOfRange<TestEnum>(enumValue, nameof(enumValue)));
        }

        [Theory]
        [InlineData(TestEnum.Budgie)]
        [InlineData(TestEnum.Cat)]
        [InlineData(TestEnum.Dog)]
        [InlineData(TestEnum.Fish)]
        [InlineData(TestEnum.Frog)]
        [InlineData(TestEnum.Penguin)]
        public void ReturnsExpectedValueGivenInRangeEnum(TestEnum enumValue)
        {
            var expected = enumValue;
            Assert.Equal(expected, Guard.Against.OutOfRange(enumValue, nameof(enumValue)));
        }

        [Theory]
        [InlineData(null, "The value of argument 'parameterName' (99) is invalid for Enum type 'TestEnum'. (Parameter 'parameterName')")]
        [InlineData("Invalid enum value", "Invalid enum value")]
        public void ErrorMessageMatchesExpectedWhenInputIsEnum(string customMessage, string expectedMessage)
        {
            var exception = Assert.Throws<InvalidEnumArgumentException>(
                () => Guard.Against.OutOfRange((TestEnum)99, "parameterName", customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "Please provide correct value")]
        [InlineData("SomeParameter", null)]
        [InlineData(null, "Value must be correct")]
        public void ExceptionParamNameMatchesExpectedWhenInputIsEnum(string expectedParamName, string customMessage)
        {
            var exception = Assert.Throws<InvalidEnumArgumentException>(
                () => Guard.Against.OutOfRange((TestEnum)99, expectedParamName, customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }

        [Theory]
        [InlineData(null, "The value of argument 'parameterName' (99) is invalid for Enum type 'TestEnum'. (Parameter 'parameterName')")]
        [InlineData("Invalid enum value", "Invalid enum value")]
        public void ErrorMessageMatchesExpectedWhenInputIsInt(string customMessage, string expectedMessage)
        {
            var exception = Assert.Throws<InvalidEnumArgumentException>(
                () => Guard.Against.OutOfRange<TestEnum>(99, "parameterName", customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "Please provide correct value")]
        [InlineData("SomeParameter", null)]
        [InlineData(null, "Value must be correct")]
        public void ExceptionParamNameMatchesExpectedWhenInputIsInt(string expectedParamName, string customMessage)
        {
            var exception = Assert.Throws<InvalidEnumArgumentException>(
                () => Guard.Against.OutOfRange<TestEnum>(99, expectedParamName, customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
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
