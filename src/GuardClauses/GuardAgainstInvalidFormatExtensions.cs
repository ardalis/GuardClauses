using System;
using System.Text.RegularExpressions;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;
using JetBrainsRegexPattern = JetBrains.Annotations.RegexPatternAttribute;
using JetBrainsInvokerParameterNameAttribute = JetBrains.Annotations.InvokerParameterNameAttribute;

namespace Ardalis.GuardClauses
{
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if  <paramref name="input"/> doesn't match the <paramref name="regexPattern"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="regexPattern"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string InvalidFormat([JetBrainsNotNull] this IGuardClause guardClause,
            [JetBrainsNotNull] string input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName,
            [JetBrainsNotNull][JetBrainsRegexPattern] string regexPattern,
            string? message = null)
        {
            var m = Regex.Match(input, regexPattern);
            if (!m.Success || input != m.Value)
            {
                throw new ArgumentException(message ?? $"Input {parameterName} was not in required format", parameterName);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if  <paramref name="input"/> doesn't satisfy the <paramref name="predicate"/> function.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="predicate"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T InvalidInput<T>([JetBrainsNotNull] this IGuardClause guardClause, [JetBrainsNotNull] T input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, Func<T, bool> predicate, string? message = null)
        {
            if (!predicate(input))
            {
                throw new ArgumentException(message ?? $"Input {parameterName} did not satisfy the options", parameterName);
            }

            return input;
        }
    }
}
