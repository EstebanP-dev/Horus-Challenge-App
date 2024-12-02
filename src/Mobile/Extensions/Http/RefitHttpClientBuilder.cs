using System.Text.Json;
using Microsoft.Extensions.Options;
using Mobile.Extensions.Http.Handlers;

namespace Mobile.Extensions.Http;
/// <summary>
/// Provides extension methods for building Refit HTTP clients.
/// </summary>
public static class RefitHttpClientBuilder
{
    /// <summary>
    /// Adds an application-specific HTTP client with the specified configuration.
    /// </summary>
    /// <typeparam name="TClient">The type of the Refit client interface.</typeparam>
    /// <typeparam name="TOptions">The type of the options used to configure the client.</typeparam>
    /// <param name="services">The service collection to add the client to.</param>
    /// <param name="configureClient">The action to configure the client with the specified options.</param>
    /// <param name="propertyNamingPolicy">The property naming policy to use for JSON serialization. (JsonNamingPolicy.CamelCase as default value)</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for configuring the client.</returns>
    public static IHttpClientBuilder AddAppHttpClient<TClient, TOptions>(
        this IServiceCollection services,
        Action<TOptions, HttpClient> configureClient,
        JsonNamingPolicy? propertyNamingPolicy = null)
        where TClient : class
        where TOptions : class
    {
        propertyNamingPolicy ??= JsonNamingPolicy.CamelCase;

        return services
            .AddClient<TClient>(propertyNamingPolicy)
            .AddConfiguration(configureClient)
            .AddDelegatingHandlers()
            .AddPrimaryMessageHandler()
            .AddLifetime();
    }

    /// <summary>
    /// Adds a Refit client to the service collection.
    /// </summary>
    /// <typeparam name="T">The type of the Refit client interface.</typeparam>
    /// <param name="builder">The service collection to add the client to.</param>
    /// <param name="propertyNamingPolicy">The property naming policy to use for JSON serialization.</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for configuring the client.</returns>
    private static IHttpClientBuilder AddClient<T>(
        this IServiceCollection builder,
        JsonNamingPolicy propertyNamingPolicy)
        where T : class
    {
        return builder
            .AddRefitClient<T>(new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = propertyNamingPolicy,
                    IncludeFields = true
                })
            });
    }

    /// <summary>
    /// Adds delegating handlers to the HTTP client.
    /// </summary>
    /// <param name="builder">The HTTP client builder to add the handlers to.</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for configuring the client.</returns>
    private static IHttpClientBuilder AddDelegatingHandlers(
        this IHttpClientBuilder builder)
    {
        return builder
            .AddHttpMessageHandler<ConnectionDelegatingHandler>()
            .AddHttpMessageHandler<RetryDelegatingHandler>()
            .AddHttpMessageHandler<HeaderDelegatingHandler>();
    }

    /// <summary>
    /// Configures the HTTP client with the specified options.
    /// </summary>
    /// <typeparam name="TOptions">The type of the options used to configure the client.</typeparam>
    /// <param name="builder">The HTTP client builder to configure.</param>
    /// <param name="configureClient">The action to configure the client with the specified options.</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for configuring the client.</returns>
    private static IHttpClientBuilder AddConfiguration<TOptions>(
            this IHttpClientBuilder builder,
            Action<TOptions, HttpClient> configureClient)
        where TOptions : class
    {
        return builder
            .ConfigureHttpClient((provider, client) =>
            {
                IOptions<TOptions>? options = provider.GetService<IOptions<TOptions>>();

                ArgumentNullException.ThrowIfNull(options);

                TOptions settings = options.Value;

                configureClient(settings, client);
            });
    }

    /// <summary>
    /// Configures the primary message handler for the HTTP client.
    /// </summary>
    /// <param name="builder">The HTTP client builder to configure.</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for configuring the client.</returns>
    private static IHttpClientBuilder AddPrimaryMessageHandler(
        this IHttpClientBuilder builder)
    {
        return builder
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(1),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
                MaxConnectionsPerServer = 10,
                EnableMultipleHttp2Connections = true
            });
    }

    /// <summary>
    /// Sets the handler lifetime for the HTTP client to infinite.
    /// </summary>
    /// <param name="builder">The HTTP client builder to configure.</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for configuring the client.</returns>
    private static IHttpClientBuilder AddLifetime(
        this IHttpClientBuilder builder)
    {
        return builder
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
    }
}
