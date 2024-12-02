namespace Mobile.Features.Challenges.GetChallenges;

internal sealed class GetChallengesQueryHandler(
        IChallengesClient client)
    : IRequestHandler<GetChallengesQuery, Result<IEnumerable<GetChallengesResponse>>>
{
    public async Task<Result<IEnumerable<GetChallengesResponse>>> Handle(GetChallengesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<GetChallengesResponse> result = await client
            .GetChallengesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Success(result);
    }
}
