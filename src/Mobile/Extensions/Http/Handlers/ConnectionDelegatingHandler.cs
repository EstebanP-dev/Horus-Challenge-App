namespace Mobile.Extensions.Http.Handlers;

/// <summary>
/// A delegating handler that checks for an active network connection before sending HTTP requests.
/// </summary>
internal sealed class ConnectionDelegatingHandler()
    : DelegatingHandler
{
    /// <summary>
    /// Sends an HTTP request to the inner handler if there is an active network connection.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>The HTTP response message from the inner handler.</returns>
    /// <exception cref="NoInternetConnectionException">Thrown when there is no active network connection.</exception>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        bool hasConnection = Connectivity.Current is { NetworkAccess: NetworkAccess.Internet or NetworkAccess.Local };

        if (!hasConnection)
        {
            throw new NoInternetConnectionException();
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
