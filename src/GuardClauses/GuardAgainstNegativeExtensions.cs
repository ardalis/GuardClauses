using System;
using System.Runtime.CompilerServices;
using JetBrainsInvokerParameterNameAttribute = JetBrains.Annotations.InvokerParameterNameAttribute;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

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
        public static int Negative([JetBrainsNotNull] this IGuardClause guardClause, 
            int input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static int Negative([JetBrainsNotNull] this IGuardClause guardClause,
            int input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static long Negative([JetBrainsNotNull] this IGuardClause guardClause, 
            long input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static long Negative([JetBrainsNotNull] this IGuardClause guardClause,
            long input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static decimal Negative([JetBrainsNotNull] this IGuardClause guardClause, 
            decimal input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static decimal Negative([JetBrainsNotNull] this IGuardClause guardClause,
            decimal input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static float Negative([JetBrainsNotNull] this IGuardClause guardClause, 
            float input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static float Negative([JetBrainsNotNull] this IGuardClause guardClause,
            float input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static double Negative([JetBrainsNotNull] this IGuardClause guardClause, 
            double input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static double Negative([JetBrainsNotNull] this IGuardClause guardClause,
            double input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static TimeSpan Negative([JetBrainsNotNull] this IGuardClause guardClause, 
            TimeSpan input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static TimeSpan Negative([JetBrainsNotNull] this IGuardClause guardClause,
            TimeSpan input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        private static T Negative<T>([JetBrainsNotNull] this IGuardClause guardClause, 
            T input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null) where T : struct, IComparable
#else
        private static T Negative<T>([JetBrainsNotNull] this IGuardClause guardClause,
            T input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static int NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, 
            int input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static int NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause,
            int input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static long NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, 
            long input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static long NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause,
            long input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static decimal NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, 
            decimal input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static decimal NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause,
            decimal input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static float NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, 
            float input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static float NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause,
            float input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static double NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause,
            double input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static double NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause,
            double input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        public static TimeSpan NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause, 
            TimeSpan input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static TimeSpan NegativeOrZero([JetBrainsNotNull] this IGuardClause guardClause,
            TimeSpan input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
        private static T NegativeOrZero<T>([JetBrainsNotNull] this IGuardClause guardClause, 
            T input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null) where T : struct, IComparable
#else
        private static T NegativeOrZero<T>([JetBrainsNotNull] this IGuardClause guardClause,
            T input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
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
