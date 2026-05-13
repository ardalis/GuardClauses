using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Ardalis.GuardClauses;

public static partial class GuardClauseExtensions
{
    /// <summary>
    /// Validates the <paramref name="input"/> using the specified <paramref name="func"/> and throws an <see cref="ArgumentException"/>.
    /// The <paramref name="func"/> should return true to indicate an invalid or undesirable state of the input.
    /// If <paramref name="func"/> returns true, an <see cref="ArgumentException"/> is thrown, signifying that the input is invalid.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <param name="guardClause">The guard clause instance.</param>
    /// <param name="func">The function that evaluates the input. It should return true if the input is considered invalid or in a negative state.</param>
    /// <param name="input">The input to evaluate.</param>
    /// <param name="message">The message to include in the exception if the input is invalid.</param>
    /// <param name="parameterName">The name of the parameter to include in the thrown exception, captured automatically from the input expression.</param>
    /// <returns>The <paramref name="input"/> if the <paramref name="func"/> evaluates to false, indicating a valid state.</returns>
    /// <exception cref="ArgumentException">Thrown when the validation function returns true, indicating that the input is invalid.</exception>
    public static T Expression<T>(this IGuardClause guardClause,
        Func<T, bool> func,
        T input,
        string message,
        [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        where T : struct
    {
        return guardClause.Expression(func, input, () => new ArgumentException(message, parameterName!));
    }

    /// <summary>
    /// Validates the <paramref name="input"/> using the specified <paramref name="func"/> and throws a custom <see cref="Exception" /> if it evaluates to true.
    /// The <paramref name="func"/> should return true to indicate an invalid or undesirable state of the input.
    /// If <paramref name="func"/> returns true, a custom <see cref="Exception" /> is thrown, signifying that the input is invalid.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <param name="guardClause">The guard clause instance.</param>
    /// <param name="func">The function that evaluates the input. It should return true if the input is considered invalid or in a negative state.</param>
    /// <param name="input">The input to evaluate.</param>
    /// <param name="createException">A factory method to create the custom exception.</param>
    /// <returns>The <paramref name="input"/> if the <paramref name="func"/> evaluates to false, indicating a valid state.</returns>
    /// <exception cref="Exception"></exception>
    public static T Expression<T>(this IGuardClause guardClause,
        Func<T, bool> func,
        T input,
        Func<Exception> createException)
        where T : struct
    {
        return func(input) ? throw createException() : input;
    }

    /// <summary>
    /// Validates the <paramref name="func"/> asynchronously and throws an <see cref="ArgumentException" /> if it evaluates to false for given <paramref name="input"/>
    /// The <paramref name="func"/> should return true to indicate an invalid or undesirable state.
    /// If <paramref name="func"/> returns true, indicating that the input is invalid, an <see cref="ArgumentException"/> is thrown.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <param name="guardClause">The guard clause instance.</param>
    /// <param name="func">The function that evaluates the input. It should return true if the input is considered invalid or in a negative state.</param>
    /// <param name="input">The input to evaluate.</param>
    /// <param name="message">The message to include in the exception if the input is invalid.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <param name="parameterName">The name of the parameter to include in the thrown exception, captured automatically from the input expression.</param>
    /// <returns><paramref name="input"/> if the <paramref name="func"/> evaluates to true </returns>
    /// <exception cref="ArgumentException">Thrown when the validation function returns true, indicating that the input is invalid.</exception>
    public static Task<T> ExpressionAsync<T>(this IGuardClause guardClause,
        Func<T, CancellationToken, Task<bool>> func,
        T input,
        string message,
        CancellationToken cancellationToken = default,
        [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        where T : struct
    {
        return guardClause.ExpressionAsync(func, input, () => new ArgumentException(message, parameterName!), cancellationToken);
    }

    /// <summary>
    /// Validates the <paramref name="func"/> asynchronously and throws a custom <see cref="Exception" /> if it evaluates to false for given <paramref name="input"/>
    /// The <paramref name="func"/> should return true to indicate an invalid or undesirable state.
    /// If <paramref name="func"/> returns true, indicating that the input is invalid, a custom <see cref="Exception" /> is thrown.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <param name="guardClause">The guard clause instance.</param>
    /// <param name="func">The function that evaluates the input. It should return true if the input is considered invalid or in a negative state.</param>
    /// <param name="input">The input to evaluate.</param>
    /// <param name="createException">A factory method to create the custom exception.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns><paramref name="input"/> if the <paramref name="func"/> evaluates to true </returns>
    /// <exception cref="ArgumentException">Thrown when the validation function returns true, indicating that the input is invalid.</exception>
    /// <exception cref="Exception"></exception>
    public static async Task<T> ExpressionAsync<T>(
        this IGuardClause guardClause,
        Func<T, CancellationToken, Task<bool>> func,
        T input,
        Func<Exception> createException,
        CancellationToken cancellationToken = default)
        where T : struct
    {
        return await func(input, cancellationToken).ConfigureAwait(false) ? throw createException() : input;
    }
}
