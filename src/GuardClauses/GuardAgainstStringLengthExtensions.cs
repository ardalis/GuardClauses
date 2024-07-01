using System;
using System.Runtime.CompilerServices;

namespace Ardalis.GuardClauses;

/// <summary>
/// The class containing extension methods for <see cref="IGuardClause"/> 
/// for <see cref="string"/> and <see cref="System.Text.StringBuilder"/> types.
/// </summary>
public static partial class GuardClauseExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentException" /> or a custom <see cref="Exception" /> if string <paramref name="input"/> is too short.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="minLength"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <param name="exceptionCreator"></param>
    /// <returns><paramref name="input" /> if the value is not negative.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public static string StringTooShort(this IGuardClause guardClause,
        string input,
        int minLength,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null,
        Func<Exception>? exceptionCreator = null)
    {
        Guard.Against.NegativeOrZero(minLength, nameof(minLength), exceptionCreator: exceptionCreator);
        if (input.Length < minLength)
        {
            Exception? exception = exceptionCreator?.Invoke();

            throw exception ?? new ArgumentException(message ?? $"Input {parameterName} with length {input.Length} is too short. Minimum length is {minLength}.", parameterName);
        }
        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException" /> or a custom <see cref="Exception" /> if string <paramref name="input"/> is too long.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="maxLength"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <param name="exceptionCreator"></param>
    /// <returns><paramref name="input" /> if the value is not negative.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public static string StringTooLong(this IGuardClause guardClause,
        string input,
        int maxLength,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null,
        Func<Exception>? exceptionCreator = null)
    {
        Guard.Against.NegativeOrZero(maxLength, nameof(maxLength), exceptionCreator: exceptionCreator);
        if (input.Length > maxLength)
        {
            Exception? exception = exceptionCreator?.Invoke();

            throw exception ?? new ArgumentException(message ?? $"Input {parameterName} with length {input.Length} is too long. Maximum length is {maxLength}.", parameterName);
        }
        return input;
    }
}
