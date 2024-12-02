namespace Mobile.Primitives;

/// <summary>
/// Represents the types of errors that can occur in the application.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// No error.
    /// </summary>
    None = 0,

    /// <summary>
    /// A general failure.
    /// </summary>
    Failure = 1,

    /// <summary>
    /// A validation error.
    /// </summary>
    Validation = 2,

    /// <summary>
    /// A problem error.
    /// </summary>
    Problem = 3,

    /// <summary>
    /// A not found error.
    /// </summary>
    NotFound = 4,

    /// <summary>
    /// A conflict error.
    /// </summary>
    Conflict = 5,

    /// <summary>
    /// An unauthorized error.
    /// </summary>
    Unauthorized = 6,

    /// <summary>
    /// A not implemented error.
    /// </summary>
    NotImplemented = 7,

    /// <summary>
    /// An unexpected error.
    /// </summary>
    Unexpected = 8,
}
