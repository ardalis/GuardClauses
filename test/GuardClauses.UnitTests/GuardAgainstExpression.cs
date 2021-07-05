using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstExpression
    {
        public struct CustomStruct
        {
            public string FieldName { get; set; }
        }

        private static IEnumerable<object[]> GetCustomStruct()
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
            Guard.Against.AgainstExpression((x) => x == 10, test, "Value is not equal to 10");
        }

        [Theory]
        [InlineData(10)]
        public void GivenIntegerWhenTheExpressionEvaluatesToFalseThrowsException(int test)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.AgainstExpression((x) => x == 5, test, "Value is not equal to 10"));
        }

        [Theory]
        [InlineData(1.1)]
        public void GivenDoubleWhenTheExpressionEvaluatesToTrueDoesNothing(double test)
        {
            Guard.Against.AgainstExpression((x) => x == 1.1, test, "Value is not equal to 1.1");
        }

        [Theory]
        [InlineData(1.1)]
        public void GivenDoubleWhenTheExpressionEvaluatesToFalseThrowsException(int test)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.AgainstExpression((x) => x == 5.0, test, "Value is not equal to 1.1"));
        }

        [Theory]
        [MemberData(nameof(GetCustomStruct))]
        public void GivenCustomStructWhenTheExpressionEvaluatesToTrueDoesNothing(CustomStruct test)
        {
            Guard.Against.AgainstExpression((x) => x.FieldName == "FieldValue", test, "FieldValue is not matching");
        }

        [Theory]
        [MemberData(nameof(GetCustomStruct))]
        public void GivenCustomStructWhenTheExpressionEvaluatesToFalseThrowsException(CustomStruct test)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.AgainstExpression((x) => x.FieldName == "FailThis", test, "FieldValue is not matching"));
        }
    }
}
