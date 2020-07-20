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
    /// Guard.WithValue(value).AgainstNull(nameof(value));
    /// </example>
    public static class GuardClauseExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <paramref name="guardClause.Value" /> is null.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IGuard<T> AgainstNull<T>([NotNull, JetBrainsNotNull] this IGuard<T> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            if (guardClause.Value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <paramref name="guardClause.Value" /> is null.
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is an empty string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not an empty string or null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<string> AgainstNullOrEmpty([NotNull, JetBrainsNotNull] this IGuard<string> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            guardClause.AgainstNull(parameterName);
            if (guardClause.Value == string.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <paramref name="guardClause.Value" /> is null.
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is an empty guid.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not an empty guid or null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<Guid> AgainstNullOrEmpty([NotNull, JetBrainsNotNull] this IGuard<Guid?> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            guardClause.AgainstNull(parameterName);
            return Guard.WithValue(guardClause.Value!.Value).AgainstEmpty(parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <paramref name="guardClause.Value" /> is null.
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is an empty enumerable.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not an empty enumerable or null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<IEnumerable<T>> AgainstNullOrEmpty<T>([NotNull, JetBrainsNotNull] this IGuard<IEnumerable<T>> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            guardClause.AgainstNull(parameterName);
            if (!guardClause.Value.Any())
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is an empty guid.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not an empty guid or null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<Guid> AgainstEmpty([NotNull, JetBrainsNotNull] this IGuard<Guid> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            if (guardClause.Value == Guid.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <paramref name="guardClause.Value" /> is null.
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is an empty or white space string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not an empty or whitespace string.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<string> AgainstNullOrWhiteSpace([NotNull, JetBrainsNotNull] this IGuard<string> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            guardClause.AgainstNullOrEmpty(parameterName);
            if (string.IsNullOrWhiteSpace(guardClause.Value))
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value" /> is less than <paramref name="rangeFrom" /> or greater than <paramref name="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="guardClause" /> if the value is inside the given range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuard<int> AgainstOutOfRange([NotNull, JetBrainsNotNull] this IGuard<int> guardClause, [NotNull, JetBrainsNotNull] string parameterName, int rangeFrom, int rangeTo)
        {
            return AgainstOutOfRange<int>(guardClause, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value" /> is less than <paramref name="rangeFrom" /> or greater than <paramref name="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="guardClause" /> if the value is inside the given range.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuard<DateTime> AgainstOutOfRange([NotNull, JetBrainsNotNull] this IGuard<DateTime> guardClause, [NotNull, JetBrainsNotNull] string parameterName, DateTime rangeFrom, DateTime rangeTo)
        {
            return AgainstOutOfRange<DateTime>(guardClause, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value" /> is not in the range of valid SqlDateTime values.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is in the range of valid SqlDateTime values.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuard<DateTime> AgainstOutOfSQLDateRange([NotNull, JetBrainsNotNull] this IGuard<DateTime> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            // System.Data is unavailable in .NET Standard so we can't use SqlDateTime.
            const long SqlMinDateTicks = 552877920000000000;
            const long SqlMaxDateTicks = 3155378975999970000;

            return AgainstOutOfRange<DateTime>(guardClause, parameterName, new DateTime(SqlMinDateTicks), new DateTime(SqlMaxDateTicks));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="guardClause" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuard<decimal> AgainstOutOfRange(this IGuard<decimal> guardClause, string parameterName, decimal rangeFrom, decimal rangeTo)
        {
            return AgainstOutOfRange<decimal>(guardClause, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="guardClause" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuard<short> AgainstOutOfRange(this IGuard<short> guardClause, string parameterName, short rangeFrom, short rangeTo)
        {
            return AgainstOutOfRange<short>(guardClause, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="guardClause" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuard<double> AgainstOutOfRange(this IGuard<double> guardClause, string parameterName, double rangeFrom, double rangeTo)
        {
            return AgainstOutOfRange<double>(guardClause, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="guardClause" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuard<float> AgainstOutOfRange(this IGuard<float> guardClause, string parameterName, float rangeFrom, float rangeTo)
        {
            return AgainstOutOfRange<float>(guardClause, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value"/> is less than <paramref name="rangeFrom"/> or greater than <paramref name="rangeTo"/>.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns><paramref name="guardClause" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static IGuard<T> AgainstOutOfRange<T>(this IGuard<T> guardClause, string parameterName, T rangeFrom, T rangeTo)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0)
            {
                throw new ArgumentException($"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}");
            }

            if (comparer.Compare(guardClause.Value, rangeFrom) < 0 || comparer.Compare(guardClause.Value, rangeTo) > 0)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Input {parameterName} was out of range");
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<int> AgainstZero([NotNull, JetBrainsNotNull] this IGuard<int> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstZero<int>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<long> AgainstZero([NotNull, JetBrainsNotNull] this IGuard<long> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstZero<long>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<decimal> AgainstZero([NotNull, JetBrainsNotNull] this IGuard<decimal> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstZero<decimal>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<float> AgainstZero([NotNull, JetBrainsNotNull] this IGuard<float> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstZero<float>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<double> AgainstZero([NotNull, JetBrainsNotNull] this IGuard<double> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstZero<double>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Zero.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static IGuard<T> AgainstZero<T>([NotNull, JetBrainsNotNull] this IGuard<T> guardClause, [NotNull, JetBrainsNotNull] string parameterName) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(guardClause.Value, default))
            {
                throw new ArgumentException($"Required input {parameterName} cannot be Zero.", parameterName);
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value"/> is Negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<int> AgainstNegative([NotNull, JetBrainsNotNull] this IGuard<int> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegative<int>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value"/> is Negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<long> AgainstNegative([NotNull, JetBrainsNotNull] this IGuard<long> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegative<long>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value"/> is Negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<decimal> AgainstNegative([NotNull, JetBrainsNotNull] this IGuard<decimal> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegative<decimal>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value"/> is Negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<float> AgainstNegative([NotNull, JetBrainsNotNull] this IGuard<float> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegative<float>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value"/> is Negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<double> AgainstNegative([NotNull, JetBrainsNotNull] this IGuard<double> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegative<double>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value"/> is Negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static IGuard<T> AgainstNegative<T>([NotNull, JetBrainsNotNull] this IGuard<T> guardClause, [NotNull, JetBrainsNotNull] string parameterName) where T : struct, IComparable
        {
            if (guardClause.Value.CompareTo(default(T)) < 0)
            {
                throw new ArgumentException($"Required input {parameterName} cannot be Negative.", parameterName);
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="guardClause.Value"/> is Negative or Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative or Zero.</returns>
        public static IGuard<int> AgainstNegativeOrZero([NotNull, JetBrainsNotNull] this IGuard<int> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegativeOrZero<int>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="guardClause.Value"/> is Negative or Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause.Value" /> if the value is not Negative or Zero.</returns>
        public static IGuard<long> AgainstNegativeOrZero([NotNull, JetBrainsNotNull] this IGuard<long> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegativeOrZero<long>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="guardClause.Value"/> is Negative or Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative or Zero.</returns>
        public static IGuard<decimal> AgainstNegativeOrZero([NotNull, JetBrainsNotNull] this IGuard<decimal> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegativeOrZero<decimal>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="guardClause.Value"/> is Negative or Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative or Zero.</returns>
        public static IGuard<float> AgainstNegativeOrZero([NotNull, JetBrainsNotNull] this IGuard<float> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegativeOrZero<float>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="guardClause.Value"/> is Negative or Zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative or Zero.</returns>
        public static IGuard<double> AgainstNegativeOrZero([NotNull, JetBrainsNotNull] this IGuard<double> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return AgainstNegativeOrZero<double>(guardClause, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="guardClause.Value"/> is Negative or Zero. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not Negative or Zero.</returns>
        private static IGuard<T> AgainstNegativeOrZero<T>([NotNull, JetBrainsNotNull] this IGuard<T> guardClause, [NotNull, JetBrainsNotNull] string parameterName) where T : struct, IComparable
        {
            if (guardClause.Value.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException($"Required input {parameterName} cannot be Zero or Negative.", parameterName);
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value"/> is not a valid enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuard<int> AgainstOutOfRange<T>([NotNull, JetBrainsNotNull] this IGuard<int> guardClause, [NotNull, JetBrainsNotNull] string parameterName) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), guardClause.Value))
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Required input {parameterName} was not a valid enum value for {typeof(T)}.");
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <paramref name="guardClause.Value"/> is not a valid enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not out of range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IGuard<T> AgainstOutOfRange<T>([NotNull, JetBrainsNotNull] this IGuard<T> guardClause, [NotNull, JetBrainsNotNull] string parameterName) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), guardClause.Value))
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Required input {parameterName} was not a valid enum value for {typeof(T)}.");
            }

            return guardClause;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="guardClause.Value" /> is default for that type.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="guardClause" /> if the value is not default for that type.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IGuard<T> AgainstDefault<T>([NotNull, JetBrainsNotNull] this IGuard<T> guardClause, [NotNull, JetBrainsNotNull] string parameterName)
        {
            if (EqualityComparer<T>.Default.Equals(guardClause.Value, default))
            {
                throw new ArgumentException($"Parameter [{parameterName}] is default value for type {typeof(T).Name}", parameterName);
            }

            return guardClause;
        }
    }
}
