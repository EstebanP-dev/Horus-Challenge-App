namespace Mobile.Features.Challenges.GetChallenges;

public sealed record GetChallengesResponse(
    Guid Id,
    string Title,
    string Description,
    int CurrentPoints,
    int TotalPoints);
