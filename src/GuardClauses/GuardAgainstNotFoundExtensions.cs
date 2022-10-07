using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Ardalis.GuardClauses
{
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        /// Throws an <see cref="NotFoundException" /> if <paramref name="input" /> with <paramref name="key" /> is not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="key"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not null.</returns>
        /// <exception cref="NotFoundException"></exception>
#if NETSTANDARD || NETFRAMEWORK
        public static T NotFound<T>(this IGuardClause guardClause,
            [NotNull][ValidatedNotNull] string key,
            [NotNull][ValidatedNotNull] T input,
            string parameterName)
#else
        public static T NotFound<T>(this IGuardClause guardClause,
            [NotNull][ValidatedNotNull] string key,
            [NotNull][ValidatedNotNull] T input,
            [CallerArgumentExpression("input")] string? parameterName = null)
#endif
        {
            guardClause.NullOrEmpty(key, nameof(key));

            if (input is null)
            {
                throw new NotFoundException(key, parameterName!);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="NotFoundException" /> if <paramref name="input" /> with <paramref name="key" /> is not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="key"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not null.</returns>
        /// <exception cref="NotFoundException"></exception>
#if NETSTANDARD || NETFRAMEWORK
        public static T NotFound<TKey, T>(this IGuardClause guardClause,
            [NotNull][ValidatedNotNull] TKey key,
            [NotNull][ValidatedNotNull] T input,
            string parameterName) where TKey : struct
#else
        public static T NotFound<TKey, T>(this IGuardClause guardClause,
            [NotNull][ValidatedNotNull] TKey key,
            [NotNull][ValidatedNotNull]T input,
            [CallerArgumentExpression("input")] string? parameterName = null) where TKey : struct
#endif
        {
            guardClause.Null(key, nameof(key));

            if (input is null)
            {
                // TODO: Can we safely consider that ToString() won't return null for struct?
                throw new NotFoundException(key.ToString()!, parameterName!);
            }

            return input;
        }
    }
}
