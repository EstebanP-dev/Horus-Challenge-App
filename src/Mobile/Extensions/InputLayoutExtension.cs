namespace Mobile.Extensions;

/// <summary>
/// Provides extension methods for setting bindings on VisualElement.
/// </summary>
internal static class InputLayoutExtension
{
    /// <summary>
    /// Sets the bindings for the IsEnabled and IsVisible properties of the VisualElement.
    /// </summary>
    /// <param name="visualElement">The VisualElement to set the bindings on.</param>
    internal static void SetVisualElementBinding(this VisualElement visualElement)
    {
        visualElement.SetBinding(
            VisualElement.IsEnabledProperty,
            nameof(VisualElement.IsEnabled),
            BindingMode.TwoWay);

        visualElement.SetBinding(
            VisualElement.IsVisibleProperty,
            nameof(VisualElement.IsVisible),
            BindingMode.TwoWay);
    }
}
