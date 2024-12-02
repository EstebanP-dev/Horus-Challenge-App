using System.ComponentModel.DataAnnotations;

namespace Mobile.Options;

/// <summary>
/// Represents the settings for the API.
/// </summary>
public sealed class ApiSettings
{
    /// <summary>
    /// Gets the base URL of the API.
    /// </summary>
    [Required]
    public required string BaseUrl { get; init; }
}
