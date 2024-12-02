using System.Text.Json.Serialization;

namespace Mobile.Features.Authentication.LogIn;

public sealed record LogInResponse(
    [property: JsonPropertyName("authorizationToken")]
    string AuthorizationToken,
    [property: JsonPropertyName("email")]
    string Email,
    [property: JsonPropertyName("firstname")]
    string FirstName,
    [property: JsonPropertyName("surname")]
    string SurName);
