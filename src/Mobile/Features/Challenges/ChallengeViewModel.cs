namespace Mobile.Features.Challenges;

public sealed partial class ChallengeViewModel : ObservableObject
{
    private ChallengeViewModel()
    {
    }

    public Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required int TotalPoints { get; init; }

    public bool Completed => CurrentPoints == TotalPoints;
    public double Percentage => TotalPoints == 0 ? 0 : (double)CurrentPoints / TotalPoints;

    [ObservableProperty]
    private int _currentPoints;

    public static ChallengeViewModel Create(
        Guid id,
        string? title = null,
        string? description = null,
        int currentPoints = 0,
        int totalPoints = 0)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        return totalPoints <= 0
            ? throw new ArgumentException(@"Total points must be greater than zero.", nameof(totalPoints))
            : new ChallengeViewModel
            {
                Id = id,
                Title = title,
                Description = description,
                TotalPoints = totalPoints,
                CurrentPoints = currentPoints
            };
    }
}
