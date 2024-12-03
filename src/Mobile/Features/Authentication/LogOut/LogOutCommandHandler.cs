namespace Mobile.Features.Authentication.LogOut;

internal sealed class LogOutCommandHandler : IRequestHandler<LogOutCommand, Result>
{
    public async Task<Result> Handle(LogOutCommand request, CancellationToken cancellationToken)
    {
        string? token = await SecureStorage.Default.GetAsync(AppSecureStorageKeys.Token);

        if (string.IsNullOrEmpty(token))
        {
            return Result.Failure(Error.Unexpected());
        }
        
        SecureStorage.Default.RemoveAll();

        return Result.Success();
    }
}
