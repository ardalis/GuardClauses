using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using JetBrainsInvokerParameterNameAttribute = JetBrains.Annotations.InvokerParameterNameAttribute;
using JetBrainsNoEnumerationAttribute = JetBrains.Annotations.NoEnumerationAttribute;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;
using JetBrainsRegexPattern = JetBrains.Annotations.RegexPatternAttribute;

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// A collection of common guard clauses, implemented as extensions.
    /// </summary>
    /// <example>
    /// Guard.Against.Null(input, nameof(input));
    /// </example>
    public static class GuardClauseExtensions
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
        public static T Null<T>([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull][JetBrainsNoEnumeration] T input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
#else
        public static T Null<T>([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull][JetBrainsNoEnumeration] T input, [JetBrainsNotNull][CallerArgumentExpression("input")] string? parameterName = null, string? message = null)
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
        public static string NullOrEmpty([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] string? input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
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
        public static Guid NullOrEmpty([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] Guid? input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
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
        public static IEnumerable<T> NullOrEmpty<T>([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] IEnumerable<T>? input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
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
        public static string NullOrWhiteSpace([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] string? input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            Guard.Against.NullOrEmpty(input, parameterName, message);
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input" /> is not in the range of valid SqlDateTime values.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is in the range of valid SqlDateTime values.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static DateTime OutOfSQLDateRange([JetBrainsNotNull] this IGuardClause guardClause, DateTime input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            // System.Data is unavailable in .NET Standard so we can't use SqlDateTime.
            const long sqlMinDateTicks = 552877920000000000;
            const long sqlMaxDateTicks = 3155378975999970000;

            return OutOfRange<DateTime>(guardClause, input, parameterName, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks), message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T OutOfRange<T>(this IGuardClause guardClause, T input, [JetBrainsInvokerParameterName] string parameterName, T rangeFrom, T rangeTo, string? message = null) where T : IComparable, IComparable<T>
        {
            if (rangeFrom.CompareTo(rangeTo) > 0)
            {
                throw new ArgumentException(message ?? $"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}", parameterName);
            }

            if (input.CompareTo(rangeFrom) < 0 || input.CompareTo(rangeTo) > 0)
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"Input {parameterName} was out of range");
                }
                throw new ArgumentOutOfRangeException(parameterName, message);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int Zero([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Zero<int>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static long Zero([JetBrainsNotNull] this IGuardClause guardClause, long input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Zero<long>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static decimal Zero([JetBrainsNotNull] this IGuardClause guardClause, decimal input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Zero<decimal>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static float Zero([JetBrainsNotNull] this IGuardClause guardClause, float input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Zero<float>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static double Zero([JetBrainsNotNull] this IGuardClause guardClause, double input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Zero<double>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static TimeSpan Zero([JetBrainsNotNull] this IGuardClause guardClause, TimeSpan input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName)
        {
            return Zero<TimeSpan>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static T Zero<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} cannot be zero.", parameterName);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int Negative([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Negative<int>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static long Negative([JetBrainsNotNull] this IGuardClause guardClause, long input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Negative<long>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static decimal Negative([JetBrainsNotNull] this IGuardClause guardClause, decimal input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Negative<decimal>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static float Negative([JetBrainsNotNull] this IGuardClause guardClause, float input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Negative<float>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static double Negative([JetBrainsNotNull] this IGuardClause guardClause, double input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Negative<double>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static TimeSpan Negative([JetBrainsNotNull] this IGuardClause guardClause, TimeSpan input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return Negative<TimeSpan>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static T Negative<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) < 0)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} cannot be negative.", parameterName);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static int NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return NegativeOrZero<int>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static long NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, long input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return NegativeOrZero<long>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static decimal NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, decimal input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return NegativeOrZero<decimal>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static float NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, float input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return NegativeOrZero<float>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static double NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, double input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return NegativeOrZero<double>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static TimeSpan NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, TimeSpan input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null)
        {
            return NegativeOrZero<TimeSpan>(guardClause, input, parameterName, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        private static T NegativeOrZero<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} cannot be zero or negative.", parameterName);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="InvalidEnumArgumentException" /> if <paramref name="input"/> is not a valid enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static int OutOfRange<T>([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new InvalidEnumArgumentException(parameterName, input, typeof(T));
                }
                throw new InvalidEnumArgumentException(message);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="InvalidEnumArgumentException" /> if <paramref name="input"/> is not a valid enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static T OutOfRange<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, string? message = null) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(input), typeof(T));
                }
                throw new InvalidEnumArgumentException(message);
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
        public static string InvalidFormat([JetBrainsNotNull] this IGuardClause guardClause, [JetBrainsNotNull] string input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, [JetBrainsNotNull][JetBrainsRegexPattern] string regexPattern, string? message = null)
        {
            if (input != Regex.Match(input, regexPattern).Value)
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


        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if  any <paramref name="input"/>'s item is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if any item is not out of range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<T> OutOfRange<T>(this IGuardClause guardClause, IEnumerable<T> input, string parameterName, T rangeFrom, T rangeTo, string? message = null) where T : IComparable, IComparable<T>
        {
            if (rangeFrom.CompareTo(rangeTo) > 0)
            {
                throw new ArgumentException(message ?? $"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}", parameterName);
            }

            if (input.Any(x => x.CompareTo(rangeFrom) < 0 || x.CompareTo(rangeTo) > 0))
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentOutOfRangeException(parameterName, message ?? $"Input {parameterName} had out of range item(s)");
                }
                throw new ArgumentOutOfRangeException(parameterName, message);
            }

            return input;
        }

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
        public static T NotFound<T>([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] string key, [NotNull, JetBrainsNotNull][ValidatedNotNull][JetBrainsNoEnumeration] T input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName)
        {
            guardClause.NullOrEmpty(key, nameof(key));

            if (input is null)
            {
                throw new NotFoundException(key, parameterName);
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
        public static T NotFound<TKey, T>([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] TKey key, [NotNull, JetBrainsNotNull][ValidatedNotNull][JetBrainsNoEnumeration] T input, [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName) where TKey : struct
        {
            guardClause.Null(key, nameof(key));

            if (input is null)
            {
                // TODO: Can we safely consider that ToString() won't return null for struct?
                throw new NotFoundException(key.ToString()!, parameterName);
            }

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
        public static T AgainstExpression<T>([JetBrainsNotNull] this IGuardClause guardClause, [JetBrainsNotNull] Func<T, bool> func, T input, string message) where T : struct
        {
            if (!func(input))
            {
                throw new ArgumentException(message);
            }

            return input;
        }
    }
}
