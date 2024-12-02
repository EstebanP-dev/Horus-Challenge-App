using Mobile.Features.Challenges.GetChallenges;

namespace Mobile.Features.Challenges;

/// <summary>
/// Interface for the Challenges Client.
/// </summary>
public interface IChallengesClient
{
    /// <summary>
    /// Asynchronously retrieves the challenges.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with the challenges.</returns>
    [Get("/Challenges")]
    Task<IEnumerable<GetChallengesResponse>> GetChallengesAsync(CancellationToken cancellationToken);
}
