using Mobile.Features.Challenges.GetChallenges;

namespace Mobile.Features.Challenges;

internal sealed partial class ChallengesPageViewModel(
        ISender sender,
        IUserDialogs userDialogs)
    : ObservableObject
{
    [ObservableProperty]
    private ChallengeViewModel? _selectedChallenge;

    [ObservableProperty]
    private ChallengeViewModelCollection _challenges = [];

    public IAsyncRelayCommand OnInitializeCommand => new AsyncRelayCommand(OnInitializeAsync);
    public IAsyncRelayCommand SelectedChallengeCommand => new AsyncRelayCommand(SelectedChallengeAsync);

    private async Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        Result<IEnumerable<GetChallengesResponse>> result;

        using (userDialogs.Loading())
        {
            result = await sender
                .Send(new GetChallengesQuery(), cancellationToken)
                .ConfigureAwait(false);

            if (result.IsSuccess)
            {
                var sortedChallenges = result.Value
                    .Select(challenge => ChallengeViewModel.Create(
                        challenge.Id,
                        challenge.Title,
                        challenge.Description,
                        challenge.CurrentPoints,
                        challenge.TotalPoints))
                    .OrderByDescending(c => c.Completed)
                    .ThenBy(c => c.Title)
                    .ToList();

                foreach (ChallengeViewModel challenge in sortedChallenges)
                {
                    Challenges.Add(challenge);
                }
            }
        }

        if (result.IsFailure)
        {
            _ = userDialogs.Toast(
                result.FirstError?.Description ?? "Error",
                TimeSpan.FromSeconds(2));
        }
    }

    private async Task SelectedChallengeAsync(CancellationToken cancellationToken)
    {
        if (SelectedChallenge is null)
        {
            return;
        }

        await userDialogs.AlertAsync(
            SelectedChallenge.Description,
            SelectedChallenge.Title,
            "Close",
            cancellationToken);

        SelectedChallenge = null;
    }
}
