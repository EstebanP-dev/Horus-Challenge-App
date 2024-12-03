namespace Mobile.Features.Challenges;

internal sealed class ChallengeViewModelCollection : ObservableCollection<ChallengeViewModel>
{
    private int _compledChallenges;
    public int CompletedChallenges
    {
        get => _compledChallenges;
        set
        {
            if (_compledChallenges.Equals(value))
            {
                return;
            }

            _compledChallenges = value;
            OnPropertyChanged(
                new System.ComponentModel.PropertyChangedEventArgs(
                    nameof(CompletedChallenges)));
        }
    }

    protected override void InsertItem(int index, ChallengeViewModel item)
    {
        base.InsertItem(index, item);

        if (item.Completed)
        {
            CompletedChallenges++;
        }
    }

    protected override void ClearItems()
    {
        base.ClearItems();

        CompletedChallenges = 0;
    }
}
