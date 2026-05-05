using System;

namespace Ardalis.GuardClauses;

public static class MalformedUriGuard
{
    public static string MalformedUri(
        this IGuardClause guardClause,
        string input,
        string? parameterName = null,
        string? message = null)
    {
        if (!Uri.TryCreate(input, UriKind.Absolute, out _))
        {
            throw new ArgumentException(message ?? "Input must be a valid absolute URI.", parameterName);
        }

        return input;
    }
}
