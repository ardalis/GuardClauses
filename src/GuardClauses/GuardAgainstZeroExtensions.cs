using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrainsInvokerParameterNameAttribute = JetBrains.Annotations.InvokerParameterNameAttribute;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Ardalis.GuardClauses
{
    public static partial class GuardClauseExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message">Optional. Custom error message</param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
#if NETSTANDARD || NETFRAMEWORK
        public static int Zero([JetBrainsNotNull] this IGuardClause guardClause, 
            int input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static int Zero([JetBrainsNotNull] this IGuardClause guardClause,
            int input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
        {
            return Zero<int>(guardClause, input, parameterName!, message);
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
#if NETSTANDARD || NETFRAMEWORK
        public static long Zero([JetBrainsNotNull] this IGuardClause guardClause, 
            long input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static long Zero([JetBrainsNotNull] this IGuardClause guardClause,
            long input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
        {
            return Zero<long>(guardClause, input, parameterName!, message);
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
#if NETSTANDARD || NETFRAMEWORK
        public static decimal Zero([JetBrainsNotNull] this IGuardClause guardClause, 
            decimal input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static decimal Zero([JetBrainsNotNull] this IGuardClause guardClause,
            decimal input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
        {
            return Zero<decimal>(guardClause, input, parameterName!, message);
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
#if NETSTANDARD || NETFRAMEWORK
        public static float Zero([JetBrainsNotNull] this IGuardClause guardClause, 
            float input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static float Zero([JetBrainsNotNull] this IGuardClause guardClause,
            float input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
        {
            return Zero<float>(guardClause, input, parameterName!, message);
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
#if NETSTANDARD || NETFRAMEWORK
        public static double Zero([JetBrainsNotNull] this IGuardClause guardClause, 
            double input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName, 
            string? message = null)
#else
        public static double Zero([JetBrainsNotNull] this IGuardClause guardClause,
            double input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null,
            string? message = null)
#endif
        {
            return Zero<double>(guardClause, input, parameterName!, message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <returns><paramref name="input" /> if the value is not zero.</returns>
        /// <exception cref="ArgumentException"></exception>
#if NETSTANDARD || NETFRAMEWORK
        public static TimeSpan Zero([JetBrainsNotNull] this IGuardClause guardClause, 
            TimeSpan input, 
            [JetBrainsNotNull][JetBrainsInvokerParameterName] string parameterName)
#else
        public static TimeSpan Zero([JetBrainsNotNull] this IGuardClause guardClause,
            TimeSpan input,
            [JetBrainsNotNull][JetBrainsInvokerParameterName][CallerArgumentExpression("input")] string? parameterName = null)
#endif
        {
            return Zero<TimeSpan>(guardClause, input, parameterName!);
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
    }
}
