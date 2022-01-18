using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrainsNoEnumerationAttribute = JetBrains.Annotations.NoEnumerationAttribute;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;
using JetBrainsInvokerParameterNameAttribute = JetBrains.Annotations.InvokerParameterNameAttribute;

namespace Ardalis.GuardClauses
{
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
#if NETSTANDARD || NETFRAMEWORK
        public static T Null<T>([JetBrainsNotNull] this IGuardClause guardClause,
            [NotNull, JetBrainsNotNull][ValidatedNotNull][JetBrainsNoEnumeration] T input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName,
            string? message = null)
#else
        public static T Null<T>([JetBrainsNotNull] this IGuardClause guardClause,
            [NotNull, JetBrainsNotNull][ValidatedNotNull][JetBrainsNoEnumeration] T input,
            [JetBrainsNotNull][CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not an empty string or null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string NullOrEmpty([JetBrainsNotNull] this IGuardClause guardClause,
            [NotNull, JetBrainsNotNull][ValidatedNotNull] string? input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName,
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
        public static Guid NullOrEmpty([JetBrainsNotNull] this IGuardClause guardClause,
            [NotNull, JetBrainsNotNull][ValidatedNotNull] Guid? input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName,
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
        public static IEnumerable<T> NullOrEmpty<T>([JetBrainsNotNull] this IGuardClause guardClause,
            [NotNull, JetBrainsNotNull][ValidatedNotNull] IEnumerable<T>? input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName,
            string? message = null)
        {
            Guard.Against.Null(input, parameterName, message);
            if (!input.Any())
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
        public static string NullOrWhiteSpace([JetBrainsNotNull] this IGuardClause guardClause,
            [NotNull, JetBrainsNotNull][ValidatedNotNull] string? input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName,
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
        public static T Default<T>([JetBrainsNotNull] this IGuardClause guardClause, [AllowNull, NotNull, JetBrainsNotNull][JetBrainsNoEnumeration] T input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)!) || input is null)
            {
                throw new ArgumentException(message ?? $"Parameter [{parameterName}] is default value for type {typeof(T).Name}", parameterName);
            }

            return input;
        }
    }
}
