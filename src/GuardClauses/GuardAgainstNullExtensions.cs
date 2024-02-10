﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Ardalis.GuardClauses;

/// <summary>
/// A collection of common guard clauses, implemented as extensions.
/// </summary>
/// <example>
/// Guard.Against.Null(input, nameof(input));
/// </example>
public static partial class GuardClauseExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <returns><paramref name="input" /> if the value is not null.</returns>
    public static T Null<T>(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull]T? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        if (input is null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(parameterName);
            }
            throw new ArgumentNullException(parameterName, message);
        }

        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
    /// </summary>
    /// <typeparam name="T">Must be a value type.</typeparam>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <returns><paramref name="input" /> if the value is not null.</returns>
    public static T Null<T>(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull]T? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null) where T : struct
    {
        if (input is null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(parameterName);
            }
            throw new ArgumentNullException(parameterName, message);
        }

        return input.Value;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
    /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty string.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <returns><paramref name="input" /> if the value is not an empty string or null.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string NullOrEmpty(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] string? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        Guard.Against.Null(input, parameterName, message);
        if (input == string.Empty)
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }

        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
    /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty guid.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <returns><paramref name="input" /> if the value is not an empty guid or null.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static Guid NullOrEmpty(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] Guid? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        Guard.Against.Null(input, parameterName, message);
        if (input == Guid.Empty)
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }

        return input.Value;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
    /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty enumerable.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <returns><paramref name="input" /> if the value is not an empty enumerable or null.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<T> NullOrEmpty<T>(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] IEnumerable<T>? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        Guard.Against.Null(input, parameterName, message);
        
        if (input is Array and { Length: 0 } //Try checking first with pattern matching because it's faster than TryGetNonEnumeratedCount on Array
#if NET6_0_OR_GREATER
            || (input.TryGetNonEnumeratedCount(out var count) && count == 0)
#endif
            || !input.Any())
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }

        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
    /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty or white space string.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <returns><paramref name="input" /> if the value is not an empty or whitespace string.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string NullOrWhiteSpace(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] string? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        Guard.Against.NullOrEmpty(input, parameterName, message);
        if (String.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }

        return input;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is default for that type.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <returns><paramref name="input" /> if the value is not default for that type.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static T Default<T>(this IGuardClause guardClause,
        [AllowNull, NotNull]T input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        if (EqualityComparer<T>.Default.Equals(input, default(T)!) || input is null)
        {
            throw new ArgumentException(message ?? $"Parameter [{parameterName}] is default value for type {typeof(T).Name}", parameterName);
        }

        return input;
    }


    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="input"/> is null
    /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> doesn't satisfy the <paramref name="predicate"/> function.
    /// </summary>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="parameterName"></param>
    /// <param name="predicate"></param>
    /// <param name="message">Optional. Custom error message</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static T NullOrInvalidInput<T>(this IGuardClause guardClause,
        [NotNull] T? input,
        string parameterName,
        Func<T, bool> predicate,
        string? message = null)
    {
        Guard.Against.Null(input, parameterName, message);

        return Guard.Against.InvalidInput(input, parameterName, predicate, message);
    }
}
