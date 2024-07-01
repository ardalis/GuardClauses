using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.VisualBasic;
using Xunit;

namespace GuardClauses.UnitTests;

public class GuardAgainstOutOfRangeForInvalidInput
{
    [Theory]
    [ClassData(typeof(CorrectClassData))]
    public void DoesNothingGivenInRangeValue<T>(T input, Func<T, bool> func)
    {
        Guard.Against.InvalidInput(input, nameof(input), func);
    }

    [Theory]
    [ClassData(typeof(CorrectAsyncClassData))]
    public async Task DoesNothingGivenInRangeValueAsync<T>(T input, Func<T, Task<bool>> func)
    {
        await Guard.Against.InvalidInputAsync(input, nameof(input), func);
    }

    [Theory]
    [ClassData(typeof(IncorrectClassData))]
    public void ThrowsGivenOutOfRangeValue<T>(T input, Func<T, bool> func)
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.InvalidInput(input, nameof(input), func));
    }

    [Theory]
    [ClassData(typeof(IncorrectClassData))]
    public void ThrowsCustomExceptionWhenSuppliedGivenOutOfRangeValue<T>(T input, Func<T, bool> func)
    {
        Exception customException = new Exception();
        Assert.Throws<Exception>(() => Guard.Against.InvalidInput(input, nameof(input), func, exceptionCreator: () => customException));
    }

    [Theory]
    [ClassData(typeof(IncorrectAsyncClassData))]
    public async Task ThrowsGivenOutOfRangeValueAsync<T>(T input, Func<T, Task<bool>> func)
    {
        await Assert.ThrowsAsync<ArgumentException>(async () => await Guard.Against.InvalidInputAsync(input, nameof(input), func));
    }

    [Theory]
    [ClassData(typeof(IncorrectAsyncClassData))]
    public async Task ThrowsCustomExceptionWhenSuppliedGivenOutOfRangeValueAsync<T>(T input, Func<T, Task<bool>> func)
    {
        Exception customException = new Exception();
        await Assert.ThrowsAsync<Exception>(async () => await Guard.Against.InvalidInputAsync(input, nameof(input), func, exceptionCreator: () => customException));
    }


    [Theory]
    [ClassData(typeof(CorrectClassData))]
    public void ReturnsExpectedValueGivenInRangeValue<T>(T input, Func<T, bool> func)
    {
        var result = Guard.Against.InvalidInput(input, nameof(input), func);
        Assert.Equal(input, result);
    }

    [Theory]
    [ClassData(typeof(CorrectAsyncClassData))]
    public async Task ReturnsExpectedValueGivenInRangeValueAsync<T>(T input, Func<T, Task<bool>> func)
    {
        var result = await Guard.Against.InvalidInputAsync(input, nameof(input), func);
        Assert.Equal(input, result);
    }

    [Theory]
    [InlineData(null, "Input parameterName did not satisfy the options (Parameter 'parameterName')")]
    [InlineData("Evaluation failed", "Evaluation failed (Parameter 'parameterName')")]
    public void ErrorMessageMatchesExpected(string? customMessage, string? expectedMessage)
    {
        var exception = Assert.Throws<ArgumentException>(() => Guard.Against.InvalidInput(10, "parameterName", x => x > 20, customMessage));
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(null, "Input parameterName did not satisfy the options (Parameter 'parameterName')")]
    [InlineData("Evaluation failed", "Evaluation failed (Parameter 'parameterName')")]
    public async Task ErrorMessageMatchesExpectedAsync(string? customMessage, string? expectedMessage)
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await Guard.Against.InvalidInputAsync(10, "parameterName", x => Task.FromResult(x > 20), customMessage));
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "Please provide correct value")]
    [InlineData("SomeParameter", null)]
    [InlineData("SomeOtherParameter", "Value must be correct")]
    public void ExceptionParamNameMatchesExpected(string? expectedParamName, string? customMessage)
    {
        var exception = Assert.Throws<ArgumentException>(() => 
            Guard.Against.InvalidInput(10, expectedParamName!, x => x > 20, customMessage));
        Assert.NotNull(exception);
        Assert.Equal(expectedParamName, exception.ParamName);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "Please provide correct value")]
    [InlineData("SomeParameter", null)]
    [InlineData("SomeOtherParameter", "Value must be correct")]
    public async Task ExceptionParamNameMatchesExpectedAsync(string? expectedParamName, string? customMessage)
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
            await Guard.Against.InvalidInputAsync(10, expectedParamName!, x => Task.FromResult(x > 20), customMessage));
        Assert.NotNull(exception);
        Assert.Equal(expectedParamName, exception.ParamName);
    }

    public class CorrectClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 20, (Func<int, bool>)((x) => x > 10) };
            yield return new object[] { DateAndTime.Now, (Func<DateTime, bool>)((x) => x > DateTime.MinValue) };
            yield return new object[] { 20.0f, (Func<float, bool>)((x) => x > 10.0f) };
            yield return new object[] { 20.0m, (Func<decimal, bool>)((x) => x > 10.0m) };
            yield return new object[] { 20.0, (Func<double, bool>)((x) => x > 10.0) };
            yield return new object[] { long.MaxValue, (Func<long, bool>)((x) => x > 1) };
            yield return new object[] { short.MaxValue, (Func<short, bool>)((x) => x > 1) };
            yield return new object[] { "abcd", (Func<string, bool>)((x) => x == x.ToLower()) };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class CorrectAsyncClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 20, (Func<int, Task<bool>>)((x) => Task.FromResult(x > 10)) };
            yield return new object[] { DateAndTime.Now, (Func<DateTime, Task<bool>>)((x) => Task.FromResult(x > DateTime.MinValue)) };
            yield return new object[] { 20.0f, (Func<float, Task<bool>>)((x) => Task.FromResult(x > 10.0f)) };
            yield return new object[] { 20.0m, (Func<decimal, Task<bool>>)((x) => Task.FromResult(x > 10.0m)) };
            yield return new object[] { 20.0, (Func<double, Task<bool>>)((x) => Task.FromResult(x > 10.0)) };
            yield return new object[] { long.MaxValue, (Func<long, Task<bool>>)((x) => Task.FromResult(x > 1)) };
            yield return new object[] { short.MaxValue, (Func<short, Task<bool>>)((x) => Task.FromResult(x > 1)) };
            yield return new object[] { "abcd", (Func<string, Task<bool>>)((x) => Task.FromResult(x == x.ToLower())) };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class IncorrectClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 20, (Func<int, bool>)((x) => x < 10) };
            yield return new object[] { DateAndTime.Now, (Func<DateTime, bool>)((x) => x > DateTime.MaxValue) };
            yield return new object[] { 20.0f, (Func<float, bool>)((x) => x > 30.0f) };
            yield return new object[] { 20.0m, (Func<decimal, bool>)((x) => x > 30.0m) };
            yield return new object[] { 20.0, (Func<double, bool>)((x) => x > 30.0) };
            yield return new object[] { long.MaxValue, (Func<long, bool>)((x) => x < 1) };
            yield return new object[] { short.MaxValue, (Func<short, bool>)((x) => x < 1) };
            yield return new object[] { "abcd", (Func<string, bool>)((x) => x == x.ToUpper()) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class IncorrectAsyncClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 20, (Func<int, Task<bool>>)((x) => Task.FromResult(x < 10)) };
            yield return new object[] { DateAndTime.Now, (Func<DateTime, Task<bool>>)((x) => Task.FromResult(x > DateTime.MaxValue)) };
            yield return new object[] { 20.0f, (Func<float, Task<bool>>)((x) => Task.FromResult(x > 30.0f)) };
            yield return new object[] { 20.0m, (Func<decimal, Task<bool>>)((x) => Task.FromResult(x > 30.0m)) };
            yield return new object[] { 20.0, (Func<double, Task<bool>>)((x) => Task.FromResult(x > 30.0)) };
            yield return new object[] { long.MaxValue, (Func<long, Task<bool>>)((x) => Task.FromResult(x < 1)) };
            yield return new object[] { short.MaxValue, (Func<short, Task<bool>>)((x) => Task.FromResult(x < 1)) };
            yield return new object[] { "abcd", (Func<string, Task<bool>>)((x) => Task.FromResult(x == x.ToUpper())) };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
