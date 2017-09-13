using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.GuardClauses;

// Using the Namespace so I don't need an extra using in my project that will prevent new extensions from showing up
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
