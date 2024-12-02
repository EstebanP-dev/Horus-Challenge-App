using Microsoft.Extensions.Configuration;

namespace Mobile.Extensions;

/// <summary>
/// Provides extension methods for adding strongly-typed configuration options.
/// </summary>
internal static class StrongTypeConfigurationExtension
{
    /// <summary>
    /// Adds strongly-typed configuration options to the service collection.
    /// </summary>
    /// <typeparam name="TService">The type of the service to configure.</typeparam>
    /// <param name="services">The service collection to add the options to.</param>
    /// <param name="configuration">The configuration to bind the options to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddModelOptions<TService>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TService : class
    {
        // Get the configuration section for the specified service type
        IConfigurationSection section = configuration.GetSection(typeof(TService).Name);

        // Add options with validation on start, bind the configuration section, and validate data annotations
        services
            .AddOptionsWithValidateOnStart<TService>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
