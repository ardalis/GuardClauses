using System;
using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;

namespace GuardClauses;

/// <summary>
/// The class containing extension methods for <see cref="IGuardClause"/> 
/// for <see cref="string"/> and <see cref="System.Text.StringBuilder"/> types.
/// </summary>
public static partial class GuardClauseExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentException" /> if string <paramref name="input"/> is too short.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="minLength"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <returns><paramref name="input" /> if the value is not negative.</returns>
    /// <exception cref="ArgumentException"></exception>
#if NETFRAMEWORK || NETSTANDARD2_0
    public static string StringTooShort(this IGuardClause guardClause,
        string input,
        int minLength,
        string parameterName,
        string? message = null)
#else
    public static string StringTooShort(this IGuardClause guardClause,
        string input,
        int minLength,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
#endif
    {
        Guard.Against.NegativeOrZero(minLength, nameof(minLength));
        if (input.Length < minLength)
        {
            throw new ArgumentException(message ?? $"Input {parameterName} with length {input.Length} is too short. Minimum length is {minLength}.", parameterName);
        }
        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException" /> if string <paramref name="input"/> is too long.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="maxLength"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <returns><paramref name="input" /> if the value is not negative.</returns>
    /// <exception cref="ArgumentException"></exception>
#if NETFRAMEWORK || NETSTANDARD2_0
    public static string StringTooLong(this IGuardClause guardClause,
        string input,
        int maxLength,
        string parameterName,
        string? message = null)
#else
    public static string StringTooLong(this IGuardClause guardClause,
        string input,
        int maxLength,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
#endif
    {
        Guard.Against.NegativeOrZero(maxLength, nameof(maxLength));
        if (input.Length > maxLength)
        {
            throw new ArgumentException(message ?? $"Input {parameterName} with length {input.Length} is too long. Maxmimum length is {maxLength}.", parameterName);
        }
        return input;
    }
}
