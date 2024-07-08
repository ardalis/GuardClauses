using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Ardalis.GuardClauses;

public static partial class GuardClauseExtensions
{
    /// <summary>
    /// Throws an <see cref="NotFoundException" /> or a custom <see cref="Exception" /> if <paramref name="input" /> with <paramref name="key" /> is not found.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="guardClause"></param>
    /// <param name="key"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="exceptionCreator"></param>
    /// <returns><paramref name="input" /> if the value is not null.</returns>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="Exception"></exception>
    public static T NotFound<T>(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] string key,
        [NotNull][ValidatedNotNull] T? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        Func<Exception>? exceptionCreator =  null)
    {
        guardClause.NullOrEmpty(key, nameof(key));

        if (input is null)
        {
            Exception? exception = exceptionCreator?.Invoke();

            throw exception ?? new NotFoundException(key, parameterName!);
        }

        return input;
    }

    /// <summary>
    /// Throws an <see cref="NotFoundException" /> or a custom <see cref="Exception" /> if <paramref name="input" /> with <paramref name="key" /> is not found.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="guardClause"></param>
    /// <param name="key"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="exceptionCreator"></param>
    /// <returns><paramref name="input" /> if the value is not null.</returns>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="Exception"></exception>
    public static T NotFound<TKey, T>(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] TKey key,
        [NotNull][ValidatedNotNull]T? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        Func<Exception>? exceptionCreator =  null) where TKey : struct
    {
        guardClause.Null(key, nameof(key));

        if (input is null)
        {
            Exception? exception = exceptionCreator?.Invoke();

            // TODO: Can we safely consider that ToString() won't return null for struct?
            throw exception ?? new NotFoundException(key.ToString()!, parameterName!);
        }

        return input;
    }
}
