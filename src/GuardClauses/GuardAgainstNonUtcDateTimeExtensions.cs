using System;
using System.Runtime.CompilerServices;
using JetBrainsInvokerParameterNameAttribute = JetBrains.Annotations.InvokerParameterNameAttribute;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Ardalis.GuardClauses
{
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> kind is not Utc.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the DateTime kind is not Utc.</returns>
        /// <exception cref="ArgumentException"></exception>
#if NETSTANDARD || NETFRAMEWORK
        public static DateTime NonUtcDateTime([JetBrainsNotNull] this IGuardClause guardClause,
            DateTime input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static DateTime NonUtcDateTime([JetBrainsNotNull] this IGuardClause guardClause,
            DateTime input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
        {
            if (input.Kind != DateTimeKind.Utc)
                throw new ArgumentException(message ?? $"Input {parameterName} kind is not Utc.", parameterName);

            return input;
        }
    }
}
