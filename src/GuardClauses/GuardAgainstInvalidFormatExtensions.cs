using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ardalis.GuardClauses;

public static partial class GuardClauseExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentException" /> or a custom <see cref="Exception" /> if  <paramref name="input"/> doesn't match the <paramref name="regexPattern"/>.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="regexPattern"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <param name="exception"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public static string InvalidFormat(this IGuardClause guardClause,
        string input,
        string parameterName,
        string regexPattern,
        string? message = null,
        Exception? exception = null)
    {
        var m = Regex.Match(input, regexPattern);
        if (!m.Success || input != m.Value)
        {
            throw exception ?? new ArgumentException(message ?? $"Input {parameterName} was not in required format", parameterName);
        }

        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException" /> or a custom <see cref="Exception" /> if  <paramref name="input"/> doesn't satisfy the <paramref name="predicate"/> function.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="predicate"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <param name="exception"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public static T InvalidInput<T>(this IGuardClause guardClause, 
        T input, string parameterName, 
        Func<T, bool> predicate, 
        string? message = null,
        Exception? exception = null)
    {
        if (!predicate(input))
        {
            throw exception ?? new ArgumentException(message ?? $"Input {parameterName} did not satisfy the options", parameterName);
        }

        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException" /> or a custom <see cref="Exception" /> if  <paramref name="input"/> doesn't satisfy the <paramref name="predicate"/> function.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="predicate"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <param name="exception"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public static async Task<T> InvalidInputAsync<T>(this IGuardClause guardClause,
        T input,
        string parameterName,
        Func<T, Task<bool>> predicate,
        string? message = null,
        Exception? exception = null)
    {
        if (!await predicate(input))
        {
            throw exception ?? new ArgumentException(message ?? $"Input {parameterName} did not satisfy the options", parameterName);
        }

        return input;
    }
}
