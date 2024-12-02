using Microsoft.Extensions.Configuration;

namespace Mobile.Extensions;

/// <summary>
/// Provides extension methods for configuring the MauiAppBuilder.
/// </summary>
internal static class ConfigurationExtensions
{
    /// <summary>
    /// Adds application configuration to the MauiAppBuilder.
    /// </summary>
    /// <param name="builder">The MauiAppBuilder instance.</param>
    /// <returns>The updated MauiAppBuilder instance.</returns>
    internal static MauiAppBuilder AddAppConfiguration(this MauiAppBuilder builder)
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddAppSettings();

        IConfigurationRoot configurationRoot = configurationBuilder.Build();

        builder.Configuration.AddConfiguration(configurationRoot);

        return builder;
    }

    /// <summary>
    /// Adds application settings to the configuration builder.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder instance.</param>
    /// <returns>The updated configuration builder instance.</returns>
    private static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder)
    {
        // ReSharper disable once StringLiteralTypo
        configurationBuilder
            .AddFile("appsettings.json");

#if DEBUG
        configurationBuilder
            .AddFile("appsettings.Development.json");
#endif

        return configurationBuilder;
    }

    /// <summary>
    /// Adds a configuration file to the configuration builder.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder instance.</param>
    /// <param name="filename">The name of the configuration file to add.</param>
    /// <returns>The updated configuration builder instance.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the specified file is not found.</exception>
    private static void AddFile(this IConfigurationBuilder configurationBuilder, string filename)
    {
        var assembly = Assembly.GetExecutingAssembly();
        string[] resourceNames = assembly
            .GetManifestResourceNames();

        string? resourceName = Array.Find(resourceNames, r => r.EndsWith(filename, StringComparison.OrdinalIgnoreCase));

        if (resourceName == null)
        {
            throw new FileNotFoundException($"Resource '{filename}' not found.");
        }

        using Stream? stream = assembly.GetManifestResourceStream(resourceName);

        if (stream == null)
        {
            throw new FileNotFoundException($"Resource '{resourceName}' not found.");
        }

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        configurationBuilder.AddConfiguration(configuration);
    }
}
