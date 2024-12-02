namespace Mobile.Features.Challenges.GetChallenges;

public sealed record GetChallengesQuery
    : IRequest<Result<IEnumerable<GetChallengesResponse>>>;
