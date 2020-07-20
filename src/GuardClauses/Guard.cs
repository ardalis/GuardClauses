namespace Ardalis.GuardClauses
{
    /// <summary>
    /// Simple interface to provide a generic mechanism to build guard clause extension methods from.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGuard<out T>
    {
        /// <summary>
        /// The value that is validated by guard clauses.
        /// </summary>
        public T Value { get; }
    }

    /// <summary>
    /// An entry point to a set of Guard Clauses defined as extension methods on IGuardClause.
    /// </summary>
    internal class Guard<T> : IGuard<T>
    {
        internal Guard(T value)
        {
            Value = value;
        }

        /// <summary>
        /// The validated value.
        /// </summary>
        public T Value { get; }
    }

    /// <summary>
    /// An entry point to a set of Guard Clauses defined as extension methods on IGuardClause.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// An entry point to a set of Guard Clauses.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IGuard<T> WithValue<T>(T value)
        {
            return new Guard<T>(value);
        }
    }
}
