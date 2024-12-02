namespace Mobile.Features.Authentication.LogIn;

public sealed record LogInCommand(
    string Email,
    string Password) : IRequest<Result>;
