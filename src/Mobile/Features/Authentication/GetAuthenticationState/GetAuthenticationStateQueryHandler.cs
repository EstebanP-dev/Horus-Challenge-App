namespace Mobile.Features.Authentication.GetAuthenticationState;

internal sealed class GetAuthenticationStateQueryHandler
    : IRequestHandler<GetAuthenticationStateQuery, Result>
{
    public async Task<Result> Handle(GetAuthenticationStateQuery request, CancellationToken cancellationToken)
    {
        string? token = await SecureStorage.Default.GetAsync(AppSecureStorageKeys.Token);

        return string.IsNullOrWhiteSpace(token)
            ? Result.Failure(Error.Failure("User is not authenticated"))
            : Result.Success();
    }
}
