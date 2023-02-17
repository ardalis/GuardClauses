using System;
using System.Runtime.CompilerServices;

namespace Ardalis.GuardClauses
{
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input"/> is negative.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not negative.</returns>
        /// <exception cref="ArgumentException"></exception>
#if NETSTANDARD || NETFRAMEWORK
        public static int Negative(this IGuardClause guardClause,
            int input,
            string parameterName,
            string? message = null)
#else
        public static int Negative(this IGuardClause guardClause,
            int input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static long Negative(this IGuardClause guardClause,
            long input,
            string parameterName,
            string? message = null)
#else
        public static long Negative(this IGuardClause guardClause,
            long input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static decimal Negative(this IGuardClause guardClause,
            decimal input,
            string parameterName,
            string? message = null)
#else
        public static decimal Negative(this IGuardClause guardClause,
            decimal input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static float Negative(IGuardClause guardClause,
            float input,
            string parameterName,
            string? message = null)
#else
        public static float Negative(this IGuardClause guardClause,
            float input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static double Negative(this IGuardClause guardClause,
            double input,
            string parameterName,
            string? message = null)
#else
        public static double Negative(this IGuardClause guardClause,
            double input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static TimeSpan Negative(this IGuardClause guardClause,
            TimeSpan input,
            string parameterName,
            string? message = null)
#else
        public static TimeSpan Negative(this IGuardClause guardClause,
            TimeSpan input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        private static T Negative<T>(this IGuardClause guardClause,
            T input,
            string parameterName,
            string? message = null) where T : struct, IComparable
#else
        private static T Negative<T>(this IGuardClause guardClause,
            T input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null) where T : struct, IComparable
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static int NegativeOrZero(this IGuardClause guardClause,
            int input,
            string parameterName,
            string? message = null)
#else
        public static int NegativeOrZero(this IGuardClause guardClause,
            int input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static long NegativeOrZero(this IGuardClause guardClause,
            long input,
            string parameterName,
            string? message = null)
#else
        public static long NegativeOrZero(this IGuardClause guardClause,
            long input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static decimal NegativeOrZero(this IGuardClause guardClause,
            decimal input,
            string parameterName,
            string? message = null)
#else
        public static decimal NegativeOrZero(this IGuardClause guardClause,
            decimal input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static float NegativeOrZero(this IGuardClause guardClause,
            float input,
            string parameterName,
            string? message = null)
#else
        public static float NegativeOrZero(this IGuardClause guardClause,
            float input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static double NegativeOrZero(this IGuardClause guardClause,
            double input,
            string parameterName,
            string? message = null)
#else
        public static double NegativeOrZero(this IGuardClause guardClause,
            double input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        public static TimeSpan NegativeOrZero(this IGuardClause guardClause,
            TimeSpan input,
            string parameterName,
            string? message = null)
#else
        public static TimeSpan NegativeOrZero(this IGuardClause guardClause,
            TimeSpan input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
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
#if NETSTANDARD || NETFRAMEWORK
        private static T NegativeOrZero<T>(this IGuardClause guardClause,
            T input,
            string parameterName,
            string? message = null) where T : struct, IComparable
#else
        private static T NegativeOrZero<T>(this IGuardClause guardClause,
            T input,
            [CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null) where T : struct, IComparable
#endif
        {
            if (input.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} cannot be zero or negative.", parameterName);
            }

            return input;
        }
    }
}
