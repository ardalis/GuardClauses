using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Ardalis.GuardClauses;

public static partial class GuardClauseExtensions
{
    /// <summary>
    /// Validates the <paramref name="input"/> using the specified <paramref name="func"/> and throws an <see cref="ArgumentException"/> or a custom <see cref="Exception" /> if it evaluates to true.
    /// The <paramref name="func"/> should return true to indicate an invalid or undesirable state of the input.
    /// If <paramref name="func"/> returns true, an <see cref="ArgumentException"/> or a custom <see cref="Exception" /> is thrown, signifying that the input is invalid.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <param name="guardClause">The guard clause instance.</param>
    /// <param name="func">The function that evaluates the input. It should return true if the input is considered invalid or in a negative state.</param>
    /// <param name="input">The input to evaluate.</param>
    /// <param name="message">The message to include in the exception if the input is invalid.</param>
    /// <param name="parameterName">The name of the parameter to include in the thrown exception, captured automatically from the input expression.</param>
    /// <param name="exceptionCreator"></param>
    /// <returns>The <paramref name="input"/> if the <paramref name="func"/> evaluates to false, indicating a valid state.</returns>
    /// <exception cref="ArgumentException">Thrown when the validation function returns true, indicating that the input is invalid.</exception>
    /// <exception cref="Exception"></exception>
    public static T Expression<T>(this IGuardClause guardClause,
        Func<T, bool> func,
        T input,
        string message,
        [CallerArgumentExpression("input")] string? parameterName = null,
        Func<Exception>? exceptionCreator = null)
        where T : struct
    {
        if (func(input))
        {
            Exception? exception = exceptionCreator?.Invoke();

            throw exception ?? new ArgumentException(message, parameterName!);
        }

        return input;
    }

    /// <summary>
    /// Validates the <paramref name="func"/> asynchronously and throws an <see cref="ArgumentException" /> or a custom <see cref="Exception" /> if it evaluates to false for given <paramref name="input"/>
    /// The <paramref name="func"/> should return true to indicate an invalid or undesirable state.
    /// If <paramref name="func"/> returns true, indicating that the input is invalid, an <see cref="ArgumentException"/> or a custom <see cref="Exception" /> is thrown.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <param name="func">The function that evaluates the input. It should return true if the input is considered invalid or in a negative state.</param>
    /// <param name="guardClause">The guard clause instance.</param>
    /// <param name="input">The input to evaluate.</param>
    /// <param name="message">The message to include in the exception if the input is invalid.</param>
    /// <param name="parameterName">The name of the parameter to include in the thrown exception, captured automatically from the input expression.</param>
    /// <param name="exceptionCreator"></param>
    /// <returns><paramref name="input"/> if the <paramref name="func"/> evaluates to true </returns>
    /// <exception cref="ArgumentException">Thrown when the validation function returns true, indicating that the input is invalid.</exception>
    /// <exception cref="Exception"></exception>
    public static async Task<T> ExpressionAsync<T>(this IGuardClause guardClause,
        Func<T, Task<bool>> func,
        T input,
        string message,
        [CallerArgumentExpression("input")] string? parameterName = null,
        Func<Exception>? exceptionCreator = null)
        where T : struct
    {
        if (await func(input))
        {
            Exception? exception = exceptionCreator?.Invoke();

            throw exception ?? new ArgumentException(message, parameterName!);
        }

        return input;
    }
}
