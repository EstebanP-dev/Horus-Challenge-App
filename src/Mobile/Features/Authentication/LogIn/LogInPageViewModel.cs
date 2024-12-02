namespace Mobile.Features.Authentication.LogIn;

internal sealed partial class LogInPageViewModel : ObservableObject
{
    [RelayCommand]
    private async Task LogInAsync()
    {
        await Shell.Current.GoToAsync(AppRoutes.Challenges);
    }
}
