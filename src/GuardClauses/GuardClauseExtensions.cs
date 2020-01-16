using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// A collection of common guard clauses, implented as extensions.
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
        public static void Null([NotNull] this IGuardClause guardClause, [NotNull] object input, [NotNull] string parameterName)
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
        public static void NullOrEmpty([NotNull] this IGuardClause guardClause, [NotNull] string input, [NotNull] string parameterName)
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
        public static void NullOrEmpty<T>([NotNull] this IGuardClause guardClause, [NotNull] IEnumerable<T> input, [NotNull] string parameterName)
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
        public static void NullOrWhiteSpace([NotNull] this IGuardClause guardClause, [NotNull] string input, [NotNull] string parameterName)
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
        public static void OutOfRange([NotNull] this IGuardClause guardClause, int input, [NotNull] string parameterName, int rangeFrom, int rangeTo)
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
        public static void OutOfRange([NotNull] this IGuardClause guardClause, DateTime input, [NotNull] string parameterName, DateTime rangeFrom, DateTime rangeTo)
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
        public static void OutOfSQLDateRange([NotNull] this IGuardClause guardClause, DateTime input, [NotNull] string parameterName)
        {
            // System.Data is unavailable in .NET Standard so we can't use SqlDateTime.
            const long sqlMinDateTicks = 552877920000000000;
            const long sqlMaxDateTicks = 3155378975999970000;

            OutOfRange<DateTime>(guardClause, input, parameterName, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <see cref="input" /> is less than <see cref="rangeFrom" /> or greater than <see cref="rangeTo" />.
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
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <see cref="input" /> is less than <see cref="rangeFrom" /> or greater than <see cref="rangeTo" />.
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
        public static void Zero([NotNull] this IGuardClause guardClause, int input, [NotNull] string parameterName)
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
        public static void Zero([NotNull] this IGuardClause guardClause, long input, [NotNull] string parameterName)
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
        public static void Zero([NotNull] this IGuardClause guardClause, decimal input, [NotNull] string parameterName)
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
        public static void Zero([NotNull] this IGuardClause guardClause, float input, [NotNull] string parameterName)
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
        public static void Zero([NotNull] this IGuardClause guardClause, double input, [NotNull] string parameterName)
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
        private static void Zero<T>([NotNull] this IGuardClause guardClause, [NotNull] T input, [NotNull] string parameterName)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
            {
                throw new ArgumentException($"Required input {parameterName} cannot be zero.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input" /> is not a valid enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange<T>([NotNull] this IGuardClause guardClause, int input, [NotNull] string parameterName) where T : Enum
        {
            if (Enum.IsDefined(typeof(T), input)) return;
            var enumName = typeof(T).ToString();
            throw new ArgumentOutOfRangeException(parameterName, $"Required input {parameterName} was not a valid enum value for {typeof(T).ToString()}.");
        }
        
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <see cref="input" /> is not a valid enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange<T>([NotNull] this IGuardClause guardClause, T input, [NotNull] string parameterName) where T : Enum
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
        public static void Default<T>([NotNull] this IGuardClause guardClause, [NotNull] T input, [NotNull] string parameterName)
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
            {
                throw new ArgumentException($"Parameter [{parameterName}] is default value for type {typeof(T).Name}", parameterName);
            }
        }
    }
}
