namespace Mobile.Exceptions;

/// <summary>
/// Exception that is thrown when there is no internet connection.
/// </summary>
#pragma warning disable CA1032
public sealed class NoInternetConnectionException
#pragma warning restore CA1032
    : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoInternetConnectionException"/> class
    /// with a default error message indicating no internet connection.
    /// </summary>
    public NoInternetConnectionException()
        : base("No internet connection.")
    {
    }
}
