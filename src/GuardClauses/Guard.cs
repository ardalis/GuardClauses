using System;

namespace Ardalis.GuardClauses
{
    public interface IGuardClause
    {
    }

    /// <summary>
    /// A collection of helper methods for implenting Guard Clauses.
    /// 
    /// </summary>
    /// <remarks>See http://www.weeklydevtips.com/004 on Guard Clauses</remarks>
    public class Guard : IGuardClause
    {
        public static IGuardClause Against { get; } = new Guard();
        public static void AgainstNull(object input, string parameterName) => Against.Null(input, parameterName);
        public static void AgainstNullOrEmpty(string input, string parameterName) => Against.NullOrEmpty(input, parameterName);
    }

    public static class CoreGuards
    {
        /// <summary>
        /// Throws an ArgumentNullException if input is null.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Null(this IGuardClause guardClause, object input, string parameterName)
        {
            if (null == input)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if input is null.
        /// Throws an ArgumentException if input is an empty string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NullOrEmpty(this IGuardClause guardClause, string input, string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (input == String.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }
    }
}
