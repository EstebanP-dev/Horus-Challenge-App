namespace Mobile.Features.Authentication.LogIn;

public sealed record UserLoggedInEvent(
        string Token)
    : INotification;
