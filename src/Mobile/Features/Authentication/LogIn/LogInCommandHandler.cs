using Mobile.Resources.Strings;

namespace Mobile.Features.Authentication.LogIn;

internal sealed class LogInCommandHandler(
        IAuthenticationClient client,
        IPublisher publisher,
        ILocalizationResourceManager resources)
    : IRequestHandler<LogInCommand, Result>
{
    public async Task<Result> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        LogInResponse? response = await client.LogInAsync(
            new LogInRequest(
                request.Email,
                request.Password),
            cancellationToken);

        if (response is null)
        {
            return Result.Failure(
                Error.Unexpected(
                    nameof(LogInCommandHandler),
                    resources[AppStringKeys.GeneralWebServiceUnexpectedResponse]));
        }

        await publisher.Publish(
            new UserLoggedInEvent(
                response.AuthorizationToken),
            cancellationToken)
            .ConfigureAwait(false);

        return Result.Success();
    }
}
