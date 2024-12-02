namespace Mobile.Primitives;

/// <summary>
/// Represents an error with a code, description, and type.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Description">The error description.</param>
/// <param name="Type">The type of the error.</param>
#pragma warning disable CA1716
public record Error(
#pragma warning restore CA1716
    string Code,
    string Description,
    ErrorType Type)
{
    /// <summary>
    /// Represents no error.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

    /// <summary>
    /// Represents a null value error.
    /// </summary>
    public static readonly Error NullValue = new("General.Null", "Null value has been provided.", ErrorType.Failure);

    /// <summary>
    /// Creates a failure error.
    /// </summary>
    /// <param name="code">The error code. Defaults to "General.Failure".</param>
    /// <param name="description">The error description. Defaults to "A 'Failure' has occurred."</param>
    /// <returns>A new <see cref="Error"/> instance representing a failure.</returns>
    public static Error Failure(
            string? code = null,
            string? description = null)
    {
        return new Error(code ?? "General.Failure", description ?? "A 'Failure' has occurred.", ErrorType.Failure);
    }

    /// <summary>
    /// Creates an unexpected error.
    /// </summary>
    /// <param name="code">The error code. Defaults to "General.Unexpected".</param>
    /// <param name="description">The error description. Defaults to "An 'Unexpected' error has occurred."</param>
    /// <returns>A new <see cref="Error"/> instance representing an unexpected error.</returns>
    public static Error Unexpected(
            string code = "General.Unexpected",
            string description = "An 'Unexpected' error has occurred.")
    {
        return new Error(code, description, ErrorType.Unexpected);
    }

    /// <summary>
    /// Creates a validation error.
    /// </summary>
    /// <param name="code">The error code. Defaults to "General.Validation".</param>
    /// <param name="description">The error description. Defaults to "A 'Validation' error has occurred."</param>
    /// <returns>A new <see cref="Error"/> instance representing a validation error.</returns>
    public static Error Validation(
            string code = "General.Validation",
            string description = "A 'Validation' error has occurred.")
    {
        return new Error(code, description, ErrorType.Validation);
    }

    /// <summary>
    /// Creates a conflict error.
    /// </summary>
    /// <param name="code">The error code. Defaults to "General.Conflict".</param>
    /// <param name="description">The error description. Defaults to "A 'Conflict' error has occurred."</param>
    /// <returns>A new <see cref="Error"/> instance representing a conflict error.</returns>
    public static Error Conflict(
            string code = "General.Conflict",
            string description = "A 'Conflict' error has occurred.")
    {
        return new Error(code, description, ErrorType.Conflict);
    }

    /// <summary>
    /// Creates a not found error.
    /// </summary>
    /// <param name="code">The error code. Defaults to "General.NotFound".</param>
    /// <param name="description">The error description. Defaults to "A 'Not Found' error has occurred."</param>
    /// <returns>A new <see cref="Error"/> instance representing a not found error.</returns>
    public static Error NotFound(
            string code = "General.NotFound",
            string description = "A 'Not Found' error has occurred.")
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    /// <summary>
    /// Creates an unauthorized error.
    /// </summary>
    /// <param name="code">The error code. Defaults to "General.Unauthorized".</param>
    /// <param name="description">The error description. Defaults to "An 'Unauthorized' error has occurred."</param>
    /// <returns>A new <see cref="Error"/> instance representing an unauthorized error.</returns>
    public static Error Unauthorized(
            string code = "General.Unauthorized",
            string description = "An 'Unauthorized' error has occurred.")
    {
        return new Error(code, description, ErrorType.Unauthorized);
    }

    /// <summary>
    /// Creates a not implemented error.
    /// </summary>
    /// <param name="code">The error code. Defaults to "General.NotImplemented".</param>
    /// <param name="description">The error description. Defaults to "A 'NotImplemented' error has occurred."</param>
    /// <returns>A new <see cref="Error"/> instance representing a not implemented error.</returns>
    public static Error NotImplemented(
            string code = "General.NotImplemented",
            string description = "A 'NotImplemented' error has occurred.")
    {
        return new Error(code, description, ErrorType.NotImplemented);
    }

    /// <summary>
    /// Creates a custom error.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    /// <param name="type">The type of the error.</param>
    /// <returns>A new <see cref="Error"/> instance representing a custom error.</returns>
    public static Error Custom(
            string code,
            string description,
            ErrorType type)
    {
        return new Error(code, description, type);
    }
}
