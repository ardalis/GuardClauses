using System;

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// A collection of helper methods for implenting Guard Clauses.
    /// 
    /// </summary>
    /// <remarks>See http://www.weeklydevtips.com/004 on Guard Clauses</remarks>
    public static partial class Guard
    {
        /// <summary>
        /// Throws an ArgumentNullException if input is null.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AgainstNull(object input, string parameterName)
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
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void AgainstNullOrEmpty(string input, string parameterName)
        {
            Guard.AgainstNull(input, parameterName);
            if(input == String.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }
    }
}
