using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.GuardClauses;

// By using the same namespace, the required using for Ardalis.GuardClauses will include custom guards regardless of location.
namespace Ardalis.GuardClauses
{
    public static class FooGuard
    {
        public static void Foo(this IGuardClause guardClause, string input, string parameterName)
        {
            if (input?.ToLower() == "foo")
                throw new ArgumentException("Should not have been foo!", parameterName);
        }
    }
}
