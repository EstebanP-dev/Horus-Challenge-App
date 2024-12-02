using Mobile.Features.Authentication.LogIn;

namespace Mobile.Features.Authentication;

/// <summary>
/// Defines the contract for the authentication client.
/// </summary>
public interface IAuthenticationClient
{
    /// <summary>
    /// Logs in a user with the specified request.
    /// </summary>
    /// <param name="request">The login request containing email and password.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the login response.</returns>
    [Post("/UserSignIn")]
    Task<LogInResponse?> LogInAsync([Body] LogInRequest request, CancellationToken cancellationToken);
}
