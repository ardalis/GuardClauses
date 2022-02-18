using System;
using System.Runtime.CompilerServices;

// By using the same namespace, the required using for Ardalis.GuardClauses will include custom guards regardless of location.
namespace Ardalis.GuardClauses;

/// <summary>
/// An example Guard extension method. Throws if input is "foo".
/// </summary>
public static class FooGuard
{
#if NETSTANDARD || NETFRAMEWORK
        public static void Foo(this IGuardClause guardClause, string input, string parameterName)
#else
    public static void Foo(this IGuardClause guardClause, string input, [CallerArgumentExpression("input")] string? parameterName = null)
#endif
    {
        if (input?.ToLower() == "foo")
            throw new ArgumentException("Should not have been foo!", parameterName);
    }
}
