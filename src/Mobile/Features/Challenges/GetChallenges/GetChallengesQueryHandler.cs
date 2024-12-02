namespace Mobile.Features.Challenges.GetChallenges;

internal sealed class GetChallengesQueryHandler
    : IRequestHandler<GetChallengesQuery, Result<IEnumerable<GetChallengesResponse>>>
{
    public async Task<Result<IEnumerable<GetChallengesResponse>>> Handle(GetChallengesQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(2000, cancellationToken).ConfigureAwait(false);

        return Result.Success(GetChallenges());
    }

    private static IEnumerable<GetChallengesResponse> GetChallenges()
    {
        return
        [
            new GetChallengesResponse(
                Guid.CreateVersion7(),
                "Challenge 1",
                "Description 1",
                100,
                100),
            new GetChallengesResponse(
                Guid.CreateVersion7(),
                "Challenge 2",
                "Description 2",
                86,
                200),
            new GetChallengesResponse(
                Guid.CreateVersion7(),
                "Challenge 3",
                "Description 3",
                300,
                300),
            new GetChallengesResponse(
                Guid.CreateVersion7(),
                "Challenge 4",
                "Description 4",
                45,
                400),
            new GetChallengesResponse(
                Guid.CreateVersion7(),
                "Challenge 5",
                "Description 5",
                357,
                500),
            new GetChallengesResponse(
                Guid.CreateVersion7(),
                "Challenge 6",
                "Description 6",
                422,
                600),
            new GetChallengesResponse(
                Guid.CreateVersion7(),
                "Challenge 7",
                "Description 7",
                700,
                700),
        ];
    }
}
