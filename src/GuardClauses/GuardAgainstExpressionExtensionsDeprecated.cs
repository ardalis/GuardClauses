using System;
using System.Threading.Tasks;

namespace Ardalis.GuardClauses;

public static partial class GuardClauseExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="func"/> evaluates to false for given <paramref name="input"/> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func"></param>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="message"></param>
    /// <returns><paramref name="input"/> if the <paramref name="func"/> evaluates to true </returns>
    /// <exception cref="ArgumentException"></exception>
    [Obsolete("Deprecated: Switch to Expression for validation (and reverse logic).")]
    public static T AgainstExpression<T>(this IGuardClause guardClause,
        Func<T, bool> func,
        T input,
        string message) where T : struct
    {
        if (!func(input)) throw new ArgumentException(message);

        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="func"/> evaluates to false for given <paramref name="input"/> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func"></param>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="message"></param>
    /// <returns><paramref name="input"/> if the <paramref name="func"/> evaluates to true </returns>
    /// <exception cref="ArgumentException"></exception>
    [Obsolete("Deprecated: Switch to ExpressionAsync for asynchronous validation (and reverse logic).")]
    public static async Task<T> AgainstExpressionAsync<T>(this IGuardClause guardClause,
        Func<T, Task<bool>> func,
        T input,
        string message) where T : struct
    {
        if (!await func(input)) throw new ArgumentException(message);

        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="func"/> evaluates to false for given <paramref name="input"/> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func"></param>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="message"></param>
    /// <param name="paramName">The name of the parameter that is invalid</param>
    /// <returns><paramref name="input"/> if the <paramref name="func"/> evaluates to true </returns>
    /// <exception cref="ArgumentException"></exception>
    [Obsolete("Deprecated: Switch to Expression for validation (and reverse logic).")]
    public static T AgainstExpression<T>(this IGuardClause guardClause, Func<T, bool> func,
        T input, string message, string paramName) where T : struct
    {
        if (!func(input)) throw new ArgumentException(message, paramName);

        return input;
    }
}
