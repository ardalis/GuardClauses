using System;

namespace Ardalis.GuardClauses;

public static partial class GuardClauseExtensions
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Throws an <see cref="ArgumentException" /> or a custom <see cref="Exception" /> if <paramref name="input" /> is an empty string.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <param name="customException">Optional. Custom exception</param>
    /// <returns><paramref name="input" /> if the value is not an empty string.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>

    public static ReadOnlySpan<char> Empty(this IGuardClause guardClause,
        ReadOnlySpan<char> input,
        string parameterName,
        string? message = null,
        Exception? customException = null)
    {
        if (input.Length == 0 || input == string.Empty)
        {
            throw customException ?? new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }
        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException" /> or a custom <see cref="Exception" /> if <paramref name="input" /> is an empty or white space string.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <param name="customException">Optional. Custom exception</param>
    /// <returns><paramref name="input" /> if the value is not an empty or whitespace string.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static ReadOnlySpan<char> WhiteSpace(this IGuardClause guardClause,
        ReadOnlySpan<char> input,
        string parameterName,
        string? message = null,
        Exception? customException = null)
    {
        if (MemoryExtensions.IsWhiteSpace(input))
        {
            throw customException ?? new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName!);
        }

        return input;
    }
#endif
}
