namespace Mobile.Features.Challenges;

internal sealed class ChallengeViewModelCollection : ObservableCollection<ChallengeViewModel>
{
    public int CompletedChallenges =>
        this.Select(x =>
            x.Completed)
        .Count();
}
