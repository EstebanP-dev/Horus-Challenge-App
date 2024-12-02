namespace Mobile.Helpers;
/// <summary>
/// Provides helper methods for validating views.
/// </summary>
public static class ViewHelper
{
    /// <summary>
    /// Validates if the provided value is of type View.
    /// </summary>
    /// <param name="bindable">The bindable object associated with the view.</param>
    /// <param name="value">The value to be validated.</param>
    /// <returns>Returns true if the value is of type View.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the value is not of type View.</exception>
    public static bool ValidateCustomView(BindableObject? bindable, object? value)
    {
        ArgumentNullException.ThrowIfNull(bindable);

        return value is not View
            ? throw new InvalidOperationException($"The value must be of type {nameof(View)}.")
            : true;
    }
}
