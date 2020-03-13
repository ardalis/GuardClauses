using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

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
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Null([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] object? input, [JetBrainsNotNull] string parameterName)
        {
            if (null == input)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NullOrEmpty([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] string? input, [JetBrainsNotNull] string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (input == String.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty enumerable.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NullOrEmpty<T>([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] IEnumerable<T>? input, [JetBrainsNotNull] string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (!input.Any())
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty or white space string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NullOrWhiteSpace([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] string? input, [JetBrainsNotNull] string parameterName)
        {
            Guard.Against.NullOrEmpty(input, parameterName);
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input" /> is less than <paramref name="rangeFrom" /> or greater than <paramref name="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName, int rangeFrom, int rangeTo)
        {
            OutOfRange<int>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input" /> is less than <paramref name="rangeFrom" /> or greater than <paramref name="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, DateTime input, [JetBrainsNotNull] string parameterName, DateTime rangeFrom, DateTime rangeTo)
        {
            OutOfRange<DateTime>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input" /> is not in the range of valid SqlDateTIme /> values.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfSQLDateRange([JetBrainsNotNull] this IGuardClause guardClause, DateTime input, [JetBrainsNotNull] string parameterName)
        {
            // System.Data is unavailable in .NET Standard so we can't use SqlDateTime.
            const long sqlMinDateTicks = 552877920000000000;
            const long sqlMaxDateTicks = 3155378975999970000;

            OutOfRange<DateTime>(guardClause, input, parameterName, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange(this IGuardClause guardClause, decimal input, string parameterName, decimal rangeFrom, decimal rangeTo)
        {
            OutOfRange<decimal>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange(this IGuardClause guardClause, short input, string parameterName, short rangeFrom, short rangeTo)
        {
            OutOfRange<short>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange(this IGuardClause guardClause, double input, string parameterName, double rangeFrom, double rangeTo)
        {
            OutOfRange<double>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange(this IGuardClause guardClause, float input, string parameterName, float rangeFrom, float rangeTo)
        {
            OutOfRange<float>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static void OutOfRange<T>(this IGuardClause guardClause, T input, string parameterName, T rangeFrom, T rangeTo)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0)
            {
                throw new ArgumentException($"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}");
            }

            if (comparer.Compare(input, rangeFrom) < 0 || comparer.Compare(input, rangeTo) > 0)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Input {parameterName} was out of range");
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName)
        {
            Zero<int>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero([JetBrainsNotNull] this IGuardClause guardClause, long input, [JetBrainsNotNull] string parameterName)
        {
            Zero<long>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero([JetBrainsNotNull] this IGuardClause guardClause, decimal input, [JetBrainsNotNull] string parameterName)
        {
            Zero<decimal>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero([JetBrainsNotNull] this IGuardClause guardClause, float input, [JetBrainsNotNull] string parameterName)
        {
            Zero<float>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero([JetBrainsNotNull] this IGuardClause guardClause, double input, [JetBrainsNotNull] string parameterName)
        {
            Zero<double>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void Zero<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull] string parameterName) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
            {
                throw new ArgumentException($"Required input {parameterName} cannot be zero.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Negative([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName)
        {
            Negative<int>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Negative([JetBrainsNotNull] this IGuardClause guardClause, long input, [JetBrainsNotNull] string parameterName)
        {
            Negative<long>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Negative([JetBrainsNotNull] this IGuardClause guardClause, decimal input, [JetBrainsNotNull] string parameterName)
        {
            Negative<decimal>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Negative([JetBrainsNotNull] this IGuardClause guardClause, float input, [JetBrainsNotNull] string parameterName)
        {
            Negative<float>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Negative([JetBrainsNotNull] this IGuardClause guardClause, double input, [JetBrainsNotNull] string parameterName)
        {
            Negative<double>(guardClause, input, parameterName);
        }
        
        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void Negative<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull] string parameterName) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) < 0)
            {
                throw new ArgumentException($"Required input {parameterName} cannot be negative.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        public static void NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName)
        {
            NegativeOrZero<int>(guardClause ,input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        public static void NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, long input, [JetBrainsNotNull] string parameterName)
        {
            NegativeOrZero<long>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        public static void NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, decimal input, [JetBrainsNotNull] string parameterName)
        {
            NegativeOrZero<decimal>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        public static void NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, float input, [JetBrainsNotNull] string parameterName)
        {
            NegativeOrZero<float>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        public static void NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, double input, [JetBrainsNotNull] string parameterName)
        {
            NegativeOrZero<double>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        private static void NegativeOrZero<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull] string parameterName) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException($"Required input {parameterName} cannot be zero or negative.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is not a valid enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange<T>([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName) where T : struct, Enum
        {
            if (Enum.IsDefined(typeof(T), input)) return;
            throw new ArgumentOutOfRangeException(parameterName, $"Required input {parameterName} was not a valid enum value for {typeof(T)}.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is not a valid enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull] string parameterName) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Required input {parameterName} was not a valid enum value for {typeof(T)}.");
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is default for that type.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Default<T>([JetBrainsNotNull] this IGuardClause guardClause, [AllowNull, NotNull, JetBrainsNotNull] T input, [JetBrainsNotNull] string parameterName)
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)!))
            {
                throw new ArgumentException($"Parameter [{parameterName}] is default value for type {typeof(T).Name}", parameterName);
            }
        }
    }
}
