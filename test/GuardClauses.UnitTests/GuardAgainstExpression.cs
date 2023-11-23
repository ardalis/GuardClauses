using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests;

public class GuardAgainstExpression
{
    public struct CustomStruct
    {
        public string FieldName { get; set; }
    }

    public static IEnumerable<object[]> GetCustomStruct()
    {
        yield return new object[] {
            new CustomStruct
            {
                FieldName = "FieldValue"
            }
        };
    }

    [Theory]
    [InlineData(10)]
    public void GivenIntegerWhenTheExpressionEvaluatesToTrueDoesNothing(int test)
    {
        Guard.Against.Expression((x) => x == 10, test, "Value is not equal to 10");
    }

    [Theory]
    [InlineData(10)]
    public void GivenIntegerWhenTheExpressionEvaluatesToFalseThrowsException(int test)
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.Expression((x) => x == 5, test, "Value is not equal to 10"));
    }

    [Theory]
    [InlineData(1.1)]
    public void GivenDoubleWhenTheExpressionEvaluatesToTrueDoesNothing(double test)
    {
        Guard.Against.Expression((x) => x == 1.1, test, "Value is not equal to 1.1");
    }

    [Theory]
    [InlineData(1.1)]
    public void GivenDoubleWhenTheExpressionEvaluatesToFalseThrowsException(int test)
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.Expression((x) => x == 5.0, test, "Value is not equal to 1.1"));
    }

    [Theory]
    [MemberData(nameof(GetCustomStruct))]
    public void GivenCustomStructWhenTheExpressionEvaluatesToTrueDoesNothing(CustomStruct test)
    {
        Guard.Against.Expression((x) => x.FieldName == "FieldValue", test, "FieldValue is not matching");
    }

    [Theory]
    [MemberData(nameof(GetCustomStruct))]
    public void GivenCustomStructWhenTheExpressionEvaluatesToFalseThrowsException(CustomStruct test)
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.Expression((x) => x.FieldName == "FailThis", test, "FieldValue is not matching"));
    }

    [Fact]
    public void ErrorIncludesParamNameIfProvided()
    {
        string paramName = "testParamName";
        var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Expression(x => x == 1, 2, "custom message", paramName));
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
        Assert.Equal(paramName, exception.ParamName);
    }
}
