using FluentValidation.Results;
using Mobile.Features.Authentication.GetAuthenticationState;
using Mobile.Features.Authentication.LogOut;

namespace Mobile.Features.Authentication.LogIn;

internal sealed partial class LogInPageViewModel(
        ILocalizationResourceManager resources,
        ISender sender,
        IUserDialogs userDialogs)
    : ObservableObject
{
    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private ValidationResult? _validationResult;

    public IAsyncRelayCommand LogInCommand => new AsyncRelayCommand(LogInAsync);
    public IAsyncRelayCommand OnInitializedCommand => new AsyncRelayCommand(OnInitializedAsync);
    public IAsyncRelayCommand OnNavigatedFromCommand => new AsyncRelayCommand(OnNavigateFromAsync);

    private async Task LogInAsync(CancellationToken cancellationToken)
    {
        var validator = new LogInPageViewModelValidator(resources);

        ValidationResult = await validator.ValidateAsync(this, cancellationToken);

        if (!ValidationResult.IsValid)
        {
            return;
        }

        if (IsBusy)
        {
            return;
        }

        Result result;

        IsBusy = true;

        using (userDialogs.Loading())
        {
            result = await sender.Send(
                    new LogInCommand(
                        Email,
                        Password),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        IsBusy = false;

        if (result.IsFailure)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                userDialogs.Toast(result.FirstError?.Description, TimeSpan.FromSeconds(2));
            });
            return;
        }

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Shell.Current.GoToAsync(AppRoutes.Challenges);
        });
    }

    private async Task OnInitializedAsync(CancellationToken cancellationToken)
    {
        Result result = await sender.Send(new GetAuthenticationStateQuery(), cancellationToken);

        if (result.IsFailure)
        {
            return;
        }

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Shell.Current.GoToAsync(AppRoutes.Challenges);
        });
    }

    private async Task OnNavigateFromAsync(CancellationToken cancellationToken)
    {
        Result result;

        using (userDialogs.Loading())
        {
            result = await sender.Send(new LogOutCommand(), cancellationToken);
        }

        if (result.IsFailure)
        {
            _ = userDialogs.Toast(result.FirstError?.Description ?? "Error", TimeSpan.FromSeconds(2));
        }
    }

    [RelayCommand]
    private async Task ShowViewNameOrTextAsync(string content)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await userDialogs.AlertAsync(content, "Has Presionado:", "Cerrar");
        });
    }
}
