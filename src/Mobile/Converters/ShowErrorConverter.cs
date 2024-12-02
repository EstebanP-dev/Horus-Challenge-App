using System.Globalization;
using FluentValidation.Results;

namespace Mobile.Converters;

/// <summary>
/// Converts a <see cref="ValidationResult"/> to an error message string for a specific property.
/// </summary>
public sealed class ShowErrorConverter : IValueConverter
{
    /// <summary>
    /// Converts a <see cref="ValidationResult"/> to an error message string for a specific property.
    /// </summary>
    /// <param name="value">The value to convert, expected to be a <see cref="ValidationResult"/>.</param>
    /// <param name="targetType">The type of the binding target property. This parameter is not used.</param>
    /// <param name="parameter">The parameter to use, expected to be the property name as a string.</param>
    /// <param name="culture">The culture to use in the converter. This parameter is not used.</param>
    /// <returns>
    /// The error message string for the specified property if an error exists; otherwise, null.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not ValidationResult validationResult || validationResult.Errors.Count == 0)
        {
            return null;
        }

        if (parameter is null)
        {
            return null;
        }

        string? property = parameter as string;

        return validationResult
            .Errors
            .SingleOrDefault(
                x => x
                    .PropertyName
                    .Split(".")
                    .LastOrDefault() == property)?
            .ErrorMessage;
    }

    /// <summary>
    /// Method not implemented. Throws a <see cref="NotImplementedException"/>.
    /// </summary>
    /// <param name="value">The value to convert back. This parameter is not used.</param>
    /// <param name="targetType">The type to convert to. This parameter is not used.</param>
    /// <param name="parameter">The parameter to use. This parameter is not used.</param>
    /// <param name="culture">The culture to use in the converter. This parameter is not used.</param>
    /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>
    /// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException("ConvertBack not implemented for the converter.");
    }
}
