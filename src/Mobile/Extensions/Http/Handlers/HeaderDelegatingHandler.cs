namespace Mobile.Extensions.Http.Handlers;
/// <summary>
/// A delegating handler that adds headers to HTTP requests.
/// </summary>
internal sealed class HeaderDelegatingHandler()
    : DelegatingHandler
{
    /// <summary>
    /// Sends an HTTP request with added headers and returns the response.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous send operation. The task result contains the HTTP response message.</returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? token = await SecureStorage.Default
            .GetAsync(AppSecureStorageKeys.Token)
            .ConfigureAwait(false);

        request.Headers.Remove("Accept");
        request.Headers.Remove("Authorization");

        request.Headers.Add("Accept", "application/json");

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Add("Authorization", token);
        }

        return await base
            .SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
    }
}
