using System;

// By using the same namespace, the required using for Ardalis.GuardClauses will include custom guards regardless of location.
namespace Ardalis.GuardClauses
{
    /// <summary>
    /// An example Guard extension method. Throws if input is "foo".
    /// </summary>
    public static class FooGuard
    {
        public static IGuard<string> AgainstFoo(this IGuard<string> guardClause, string parameterName)
        {
            if (guardClause.Value?.ToLower() == "foo")
                throw new ArgumentException("Should not have been foo!", parameterName);

            return guardClause;
        }
    }
}
