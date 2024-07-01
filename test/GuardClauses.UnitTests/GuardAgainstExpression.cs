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

    [Fact]
    public void GivenIntegerWhenTheExpressionEvaluatesToTrueThrowsException()
    {
        int testCase = 10;
        Assert.Throws<ArgumentException>(() => Guard.Against.Expression((x) => x == 10, testCase, "Value cannot be 10"));
    }

    [Fact]
    public void GivenIntegerWhenTheExpressionEvaluatesToTrueThrowsCustomExceptionWhenSupplied()
    {
        Exception customException = new Exception();
        int testCase = 10;
        Assert.Throws<Exception>(() => Guard.Against.Expression((x) => x == 10, testCase, "Value cannot be 10", exceptionCreator: () => customException));
    }


    [Fact]
    public void GivenIntegerWhenTheExpressionEvaluatesToFalseDoesNothing()
    {
        int testCase = 10;
        Guard.Against.Expression((x) => x == 5, testCase, "Value cannot be 5");
    }

    [Fact]
    public void GivenDoubleWhenTheExpressionEvaluatesToTrueThrowsException()
    {
        double testCase = 1.1;
        Assert.Throws<ArgumentException>(() => Guard.Against.Expression((x) => x == 1.1, testCase, "Value cannot be 1.1"));
    }

    [Fact]
    public void GivenDoubleWhenTheExpressionEvaluatesToTrueThrowsCustomExceptionWhenSupplied()
    {
        Exception customException = new Exception();
        double testCase = 1.1;
        Assert.Throws<Exception>(() => Guard.Against.Expression((x) => x == 1.1, testCase, "Value cannot be 1.1", exceptionCreator: () => customException));
    }


    [Fact]
    public void GivenDoubleWhenTheExpressionEvaluatesToFalseDoesNothing()
    {
        double testCase = 1.1;
        Guard.Against.Expression((x) => x == 5.0, testCase, "Value cannot be 5.0");
    }

    [Theory]
    [MemberData(nameof(GetCustomStruct))]
    public void GivenCustomStructWhenTheExpressionEvaluatesToTrueThrowsException(CustomStruct test)
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.Expression((x) => x.FieldName == "FieldValue", test, "FieldValue is not matching"));
    }

    [Theory]
    [MemberData(nameof(GetCustomStruct))]
    public void GivenCustomStructWhenTheExpressionEvaluatesToTrueThrowsCustomExceptionWhenSupplied(CustomStruct test)
    {
        Exception customException = new Exception();
        Assert.Throws<Exception>(() => Guard.Against.Expression((x) => x.FieldName == "FieldValue", test, "FieldValue is not matching", exceptionCreator: () => customException));
    }


    [Theory]
    [MemberData(nameof(GetCustomStruct))]
    public void GivenCustomStructWhenTheExpressionEvaluatesToFalseDoesNothing(CustomStruct test)
    {
        Guard.Against.Expression((x) => x.FieldName == "FailThis", test, "FieldValue is not matching");
    }

    [Fact]
    public void ErrorIncludesParamNameIfProvided()
    {
        string paramName = "testParamName";
        var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Expression(x => x == 2, 2, "custom message", paramName));
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
        Assert.Equal(paramName, exception.ParamName);
    }

}
