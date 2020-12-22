using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
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
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not null.</returns>
        public static T Null<T>([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] T input, [JetBrainsNotNull] string parameterName)
        {
            if (input is null)
            {
                throw new ArgumentNullException(parameterName);
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
        /// <returns><paramref name="input" /> if the value is not an empty string or null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string NullOrEmpty([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] string? input, [JetBrainsNotNull] string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (input == String.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
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
        /// <returns><paramref name="input" /> if the value is not an empty guid or null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static Guid NullOrEmpty([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] Guid? input, [JetBrainsNotNull] string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (input == Guid.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
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
        /// <returns><paramref name="input" /> if the value is not an empty enumerable or null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<T> NullOrEmpty<T>([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] IEnumerable<T>? input, [JetBrainsNotNull] string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (!input.Any())
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
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
        /// <returns><paramref name="input" /> if the value is not an empty or whitespace string.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string NullOrWhiteSpace([JetBrainsNotNull] this IGuardClause guardClause, [NotNull, JetBrainsNotNull][ValidatedNotNull] string? input, [JetBrainsNotNull] string parameterName)
        {
            Guard.Against.NullOrEmpty(input, parameterName);
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input" /> is less than <paramref name="rangeFrom" /> or greater than <paramref name="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if the value is inside the given range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName, int rangeFrom, int rangeTo)
        {
            return OutOfRange<int>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input" /> is less than <paramref name="rangeFrom" /> or greater than <paramref name="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if the value is inside the given range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static DateTime OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, DateTime input, [JetBrainsNotNull] string parameterName, DateTime rangeFrom, DateTime rangeTo)
        {
            return OutOfRange<DateTime>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input" /> is not in the range of valid SqlDateTime values.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is in the range of valid SqlDateTime values.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static DateTime OutOfSQLDateRange([JetBrainsNotNull] this IGuardClause guardClause, DateTime input, [JetBrainsNotNull] string parameterName)
        {
            // System.Data is unavailable in .NET Standard so we can't use SqlDateTime.
            const long sqlMinDateTicks = 552877920000000000;
            const long sqlMaxDateTicks = 3155378975999970000;

            return OutOfRange<DateTime>(guardClause, input, parameterName, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static decimal OutOfRange(this IGuardClause guardClause, decimal input, string parameterName, decimal rangeFrom, decimal rangeTo)
        {
            return OutOfRange<decimal>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static short OutOfRange(this IGuardClause guardClause, short input, string parameterName, short rangeFrom, short rangeTo)
        {
            return OutOfRange<short>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double OutOfRange(this IGuardClause guardClause, double input, string parameterName, double rangeFrom, double rangeTo)
        {
            return OutOfRange<double>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static float OutOfRange(this IGuardClause guardClause, float input, string parameterName, float rangeFrom, float rangeTo)
        {
            return OutOfRange<float>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="input"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static T OutOfRange<T>(this IGuardClause guardClause, T input, string parameterName, T rangeFrom, T rangeTo)
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

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int Zero([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName)
        {
            return Zero<int>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static long Zero([JetBrainsNotNull] this IGuardClause guardClause, long input, [JetBrainsNotNull] string parameterName)
        {
            return Zero<long>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static decimal Zero([JetBrainsNotNull] this IGuardClause guardClause, decimal input, [JetBrainsNotNull] string parameterName)
        {
            return Zero<decimal>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static float Zero([JetBrainsNotNull] this IGuardClause guardClause, float input, [JetBrainsNotNull] string parameterName)
        {
            return Zero<float>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static double Zero([JetBrainsNotNull] this IGuardClause guardClause, double input, [JetBrainsNotNull] string parameterName)
        {
            return Zero<double>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static T Zero<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull] string parameterName) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
            {
                throw new ArgumentException($"Required input {parameterName} cannot be zero.", parameterName);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int Negative([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName)
        {
            return Negative<int>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static long Negative([JetBrainsNotNull] this IGuardClause guardClause, long input, [JetBrainsNotNull] string parameterName)
        {
            return Negative<long>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static decimal Negative([JetBrainsNotNull] this IGuardClause guardClause, decimal input, [JetBrainsNotNull] string parameterName)
        {
            return Negative<decimal>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static float Negative([JetBrainsNotNull] this IGuardClause guardClause, float input, [JetBrainsNotNull] string parameterName)
        {
            return Negative<float>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static double Negative([JetBrainsNotNull] this IGuardClause guardClause, double input, [JetBrainsNotNull] string parameterName)
        {
            return Negative<double>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static T Negative<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull] string parameterName) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) < 0)
            {
                throw new ArgumentException($"Required input {parameterName} cannot be negative.", parameterName);
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static int NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<int>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static long NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, long input, [JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<long>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static decimal NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, decimal input, [JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<decimal>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static float NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, float input, [JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<float>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        public static double NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, double input, [JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<double>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="input"/> is negative or zero. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not negative or zero.</returns>
        private static T NegativeOrZero<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull] string parameterName) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException($"Required input {parameterName} cannot be zero or negative.", parameterName);
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
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static int OutOfRange<T>([JetBrainsNotNull] this IGuardClause guardClause, int input, [JetBrainsNotNull] string parameterName) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(input), typeof(T));
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
        /// <returns><paramref name="input" /> if the value is not out of range.</returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static T OutOfRange<T>([JetBrainsNotNull] this IGuardClause guardClause, T input, [JetBrainsNotNull] string parameterName) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(input), typeof(T));
            }

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is default for that type.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not default for that type.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static T Default<T>([JetBrainsNotNull] this IGuardClause guardClause, [AllowNull, NotNull, JetBrainsNotNull] T input, [JetBrainsNotNull] string parameterName)
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)!) || input is null)
            {
                throw new ArgumentException($"Parameter [{parameterName}] is default value for type {typeof(T).Name}", parameterName);
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
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string InvalidFormat([JetBrainsNotNull] this IGuardClause guardClause, [JetBrainsNotNull] string input, [JetBrainsNotNull] string parameterName, [JetBrainsNotNull] string regexPattern)
        {
            if (input != Regex.Match(input, regexPattern).Value)
                throw new ArgumentException($"Input {parameterName} was not in required format", parameterName);

            return input;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if  <paramref name="input"/> doesn't satisfy the <paramref name="predicate"/> function.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T InvalidInput<T>([JetBrainsNotNull] this IGuardClause guardClause, [JetBrainsNotNull] T input, [JetBrainsNotNull] string parameterName, Func<T, bool> predicate)
        {
            if (!predicate(input))
                throw new ArgumentException($"Input {parameterName} did not satisfy the options", parameterName);

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
        /// <returns><paramref name="input" /> if any item is not out of range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static IEnumerable<T> OutOfRange<T>(this IGuardClause guardClause, IEnumerable<T> input, string parameterName, T rangeFrom, T rangeTo) where T : IComparable
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0)
            {
                throw new ArgumentException($"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}");
            }

            if (input.Any(x => comparer.Compare(x, rangeFrom) < 0 || comparer.Compare(x, rangeTo) > 0))
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Input {parameterName} had out of range item(s)");
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
        /// <returns><paramref name="input" /> if any item is not out of range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<int> OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, IEnumerable<int> input, [JetBrainsNotNull] string parameterName, int rangeFrom, int rangeTo)
        {
            return OutOfRange<int>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if  any <paramref name="input"/>'s item is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if any item is not out of range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<long> OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, IEnumerable<long> input, [JetBrainsNotNull] string parameterName, long rangeFrom, long rangeTo)
        {
            return OutOfRange<long>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if  any <paramref name="input"/>'s item is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if any item is not out of range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<decimal> OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, IEnumerable<decimal> input, [JetBrainsNotNull] string parameterName, decimal rangeFrom, decimal rangeTo)
        {
            return OutOfRange<decimal>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if  any <paramref name="input"/>'s item is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if any item is not out of range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<float> OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, IEnumerable<float> input, [JetBrainsNotNull] string parameterName, float rangeFrom, float rangeTo)
        {
            return OutOfRange<float>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if  any <paramref name="input"/>'s item is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if any item is not out of range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<double> OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, IEnumerable<double> input, [JetBrainsNotNull] string parameterName, double rangeFrom, double rangeTo)
        {
            return OutOfRange<double>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if  any <paramref name="input"/>'s item is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="input" /> if any item is not out of range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<short> OutOfRange([JetBrainsNotNull] this IGuardClause guardClause, IEnumerable<short> input, [JetBrainsNotNull] string parameterName, short rangeFrom, short rangeTo)
        {
            return OutOfRange<short>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }
    }
}
