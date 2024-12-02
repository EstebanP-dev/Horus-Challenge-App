namespace Mobile.MarkupExtensions;

/// <summary>
/// Markup extension to resolve a ViewModel from the service provider.
/// </summary>
[ContentProperty(nameof(ViewModel))]
[AcceptEmptyServiceProvider]
public sealed class ResolveViewModel : IMarkupExtension
{
    /// <summary>
    /// Gets or sets the type of the ViewModel to resolve.
    /// </summary>
    public Type ViewModel { get; set; }

    /// <summary>
    /// Provides the value of the ViewModel from the service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve the ViewModel from.</param>
    /// <returns>The resolved ViewModel instance.</returns>
    public object ProvideValue(IServiceProvider serviceProvider)
    {
        IServiceProvider? sp = AppServiceProvider.ServiceProvider;

        return sp?.GetRequiredService(ViewModel);
    }
}
