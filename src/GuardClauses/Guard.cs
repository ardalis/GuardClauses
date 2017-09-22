using System;

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// Simple interface to provide a generic mechanism to build guard clause extension methods from.
    /// </summary>
    public interface IGuardClause
    {
    }

    /// <summary>
    /// A collection of helper methods for implementing Guard Clauses.
    /// 
    /// </summary>
    /// <remarks>See http://www.weeklydevtips.com/004 on Guard Clauses</remarks>
    public class Guard : IGuardClause
    {
        public static IGuardClause Against { get; } = new Guard();

        private Guard() { }

        /// <summary>
        /// Throws an ArgumentNullException if input is null.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AgainstNull(object input, string parameterName) => Against.Null(input, parameterName);

        /// <summary>
        /// Throws an ArgumentNullException if input is null.
        /// Throws an ArgumentException if input is an empty string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void AgainstNullOrEmpty(string input, string parameterName) => Against.NullOrEmpty(input, parameterName);

        /// <summary>
        /// Throws an ArgumentOutOfRange if input is less than <see cref="rangeFrom"/> or greater than <see cref="rangeTo"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        public static void AgainsOutOfRange(int input, string parameterName, int rangeFrom, int rangeTo)
            => Against.OutOfRange(input, parameterName, rangeFrom, rangeTo);
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

        /// <summary>
        /// Throws an ArgumentOutOfRange if input is less than <see cref="rangeFrom"/> or greater than <see cref="rangeTo"/>
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        public static void OutOfRange(this IGuardClause guardClause, int input, string parameterName,
            int rangeFrom, int rangeTo)
        {
            if (input < rangeFrom || input > rangeTo)
            {
                throw new ArgumentOutOfRangeException($"Input {parameterName} was out of range", parameterName);
            }
        }
    }
}
