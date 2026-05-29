namespace Ardalis.GuardClauses;

public static partial class Guard
{
    /// <summary>
    /// Throws if the input is not a valid absolute URI.
    /// </summary>
    public static string AgainstMalformedUri(this IGuardClause guardClause,
        string input, string parameterName)
    {
        if (!Uri.TryCreate(input, UriKind.Absolute, out _))
            throw new ArgumentException("Input is not a valid absolute URI.", parameterName);

        return input;
    }
}
