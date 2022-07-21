using System;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using JetBrainsInvokerParameterNameAttribute = JetBrains.Annotations.InvokerParameterNameAttribute;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;
using JetBrainsRegexPattern = JetBrains.Annotations.RegexPatternAttribute;

namespace GuardClauses
{
    public static partial class GuardClauseExtensions
    {
        private static readonly Regex _regex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.Compiled);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is invalid email address.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string InvalidEmailFormat([JetBrainsNotNull] this IGuardClause guardClause,
            [JetBrainsNotNull] string input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName,
            string? message = null)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(message ?? $"Input {parameterName} was null or empty", parameterName);
            }

            if (input.Length > 100)
            {
                throw new ArgumentException(message ?? $"Input {parameterName} was not in valid length", parameterName);
            }

            input = input.ToLowerInvariant();
            if (!_regex.IsMatch(input))
            {
                throw new ArgumentException(message ?? $"Input {parameterName} was not in valid format", parameterName);
            }

            return input;
        }
    }
}
