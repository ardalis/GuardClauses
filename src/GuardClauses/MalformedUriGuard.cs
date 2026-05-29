namespace Ardalis.GuardClauses;

public static partial class Guard
{
    /// <summary>
    /// Throws if the input is null, not a valid URI, or not an HTTP/HTTPS URI.
    /// </summary>
    public static string AgainstMalformedUri(this IGuardClause guardClause,
        string input, string parameterName)
    {
        if (input is null)
            throw new ArgumentNullException(parameterName);

        if (!Uri.TryCreate(input, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            throw new ArgumentException("Input is not a valid HTTP or HTTPS URI.", parameterName);

        return input;
    }
}
