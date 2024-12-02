using Polly;
using Polly.Retry;

namespace Mobile.Extensions.Http.Handlers;

/// <summary>
/// A delegating handler that applies a retry policy to HTTP requests.
/// </summary>
internal sealed class RetryDelegatingHandler : DelegatingHandler
{
    /// <summary>
    /// The retry policy to be applied to HTTP requests.
    /// Retries up to 3 times with an increasing delay between attempts.
    /// </summary>
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy =
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                3,
                attempt => TimeSpan.FromMilliseconds(50 * attempt));

    /// <summary>
    /// Sends an HTTP request with the retry policy applied.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>The HTTP response message.</returns>
    /// <exception cref="HttpRequestException">Thrown if the request fails after all retry attempts.</exception>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        PolicyResult<HttpResponseMessage> policyResult = await _retryPolicy
            .ExecuteAndCaptureAsync(
                () => base.SendAsync(request, cancellationToken))
            .ConfigureAwait(false);

        if (policyResult.Outcome == OutcomeType.Failure)
        {
            throw policyResult.FinalException;
        }

        return policyResult.Result;
    }
}
