namespace Mobile.Features.Authentication.LogIn;

internal sealed class UserLoggedInEventHandler : INotificationHandler<UserLoggedInEvent>
{
    public async Task Handle(UserLoggedInEvent notification, CancellationToken cancellationToken)
    {
        SecureStorage.Default.Remove(AppSecureStorageKeys.Token);
        await SecureStorage.Default.SetAsync(AppSecureStorageKeys.Token, notification.Token);
    }
}
