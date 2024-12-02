namespace Mobile.Features.Authentication.LogIn;

public sealed record LogInRequest(
    string Email,
    string Password);
