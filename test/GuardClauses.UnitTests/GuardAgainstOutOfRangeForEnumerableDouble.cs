﻿using System;
using System.Collections;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForEnumerableDouble
    {
        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void DoesNothingGivenInRangeValue(IEnumerable<double> input, double rangeFrom, double rangeTo)
        {
            Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo);
        }

        [Theory]
        [ClassData(typeof(IncorrectClassData))]
        public void ThrowsGivenOutOfRangeValue(IEnumerable<double> input, double rangeFrom, double rangeTo)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo));
        }

        [Theory]
        [ClassData(typeof(IncorrectRangeClassData))]
        public void ThrowsGivenInvalidArgumentValue(IEnumerable<double> input, double rangeFrom, double rangeTo)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo));
        }

        [Theory]
        [ClassData(typeof(CorrectClassData))]
        public void ReturnsExpectedValueGivenInRangeValue(IEnumerable<double> input, double rangeFrom, double rangeTo)
        {
            var result = Guard.Against.OutOfRange(input, nameof(input), rangeFrom, rangeTo);
            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData(null, "rangeFrom should be less or equal than rangeTo (Parameter 'parameterName')")]
        [InlineData("Timespan range", "Timespan range (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpectedWhenRangeIsInvalid(string customMessage, string expectedMessage)
        {
            var input = new [] { 0d, 1d, 99d };
            double rangeFrom = 2d;
            double rangeTo = 1d;

            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, "parameterName", rangeFrom, rangeTo, customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "Please provide correct value")]
        [InlineData("SomeParameter", null)]
        [InlineData("SomeOtherParameter", "Value must be correct")]
        public void ExceptionParamNameMatchesExpectedRangeIsInvalid(string expectedParamName, string customMessage)
        {
            var input = new[] { 0d, 1d, 99d };
            double rangeFrom = 2d;
            double rangeTo = 1d;

            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(input, expectedParamName, rangeFrom, rangeTo, customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }

        [Theory]
        [InlineData(null, "Input parameterName had out of range item(s) (Parameter 'parameterName')")]
        [InlineData("Timespan range", "Timespan range (Parameter 'parameterName')")]
        public void ErrorMessageMatchesExpectedWhenInputIsInvalid(string customMessage, string expectedMessage)
        {
            var input = new[] { 0d, 1d, 99d };
            double rangeFrom = 0d;
            double rangeTo = 1d;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, "parameterName", rangeFrom, rangeTo, customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "Please provide correct value")]
        [InlineData("SomeParameter", null)]
        [InlineData("SomeOtherParameter", "Value must be correct")]
        public void ExceptionParamNameMatchesExpectedInputIsInvalid(string expectedParamName, string customMessage)
        {
            var input = new[] { 0d, 1d, 99d };
            double rangeFrom = 0d;
            double rangeTo = 1d;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, expectedParamName, rangeFrom, rangeTo, customMessage));
            Assert.NotNull(exception);
            Assert.Equal(expectedParamName, exception.ParamName);
        }

        public class CorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new List<double> {double.MaxValue, 1.0, double.MinValue}, double.MinValue, double.MaxValue
                };
                yield return new object[] {new List<double> {1100000.0, 2000.8, 120.5, 180000.0}, 100.0, 1200000.0};
                yield return new object[] {new List<double> {0.1, 128, 200.5}, 0.1, 200.5};
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<double> { 10.0, 12.0, 1500.0 }, 10.0, 1200.0 };
                yield return new object[] { new List<double> { 1000.0, 200.0, 120.0, 180000.0 }, 100.0, 150000.0 };
                yield return new object[] { new List<double> { 15.0, 120.0, 158.0 }, 10.1, 110.0 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class IncorrectRangeClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<double> { 10000.0, 1200.0, 15.0}, 10000.0, 10.0 };
                yield return new object[] { new List<double> { 100010.0, 200.0, 120000.0, 0.2 }, 2000000.0, 150.0 };
                yield return new object[] { new List<double> { 52000.0, 86.0, 2500000.0 }, 20000000.0, 100.0 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }
}
