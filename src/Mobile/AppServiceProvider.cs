namespace Mobile;

/// <summary>
/// Provides a static access point to the application's <see cref="IServiceProvider" />.
/// Allows retrieval of services and creation of scopes from anywhere in the application.
/// </summary>
public sealed class AppServiceProvider
{
    /// <summary>
    /// Lazy initialization of the application's <see cref="IServiceProvider" />.
    /// Retrieves the service provider from the current <see cref="Application" /> instance.
    /// </summary>
    private static readonly Lazy<IServiceProvider?> Lazy = new(() =>
        Application
            .Current?
            .Handler?
            .MauiContext?
            .Services);

    /// <summary>
    /// Gets the application's <see cref="IServiceProvider" /> instance.
    /// </summary>
    public static IServiceProvider? ServiceProvider => AppServiceProvider.Lazy.Value;

    /// <summary>
    /// Retrieves a service of the specified type from the application's service provider.
    /// </summary>
    /// <typeparam name="TService">The type of service to retrieve.</typeparam>
    /// <returns>An instance of the requested service, or <c>null</c> if not found.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the service provider is not available.</exception>
    public static TService? GetService<TService>()
    {
        ArgumentNullException.ThrowIfNull(ServiceProvider);

        return ServiceProvider.GetService<TService>();
    }

    /// <summary>
    /// Creates a new <see cref="IServiceScope" /> from the application's service provider.
    /// </summary>
    /// <returns>A new service scope.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the service provider is not available.</exception>
    public static IServiceScope CreateScope()
    {
        ArgumentNullException.ThrowIfNull(ServiceProvider);

        return ServiceProvider.CreateScope();
    }

    /// <summary>
    /// Retrieves a service of the specified type from the application's service provider.
    /// </summary>
    /// <param name="serviceType">The type of service to retrieve.</param>
    /// <returns>An instance of the requested service, or <c>null</c> if not found.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the service provider is not available or if <paramref name="serviceType" /> is <c>null</c>.
    /// </exception>
    public static object? GetService(Type? serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(ServiceProvider);

        return AppServiceProvider.ServiceProvider.GetService(serviceType);
    }
}
