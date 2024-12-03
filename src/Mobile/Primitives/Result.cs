namespace Mobile.Primitives;

/// <summary>
/// Represents the result of an operation, containing success status and errors if any.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
#pragma warning disable IDE0055
    public bool IsFailure => !IsSuccess;
#pragma warning restore IDE0055

    /// <summary>
    /// Gets the errors associated with the result.
    /// </summary>
#pragma warning disable CA1819
    public Error[] Errors { get; }
#pragma warning restore CA1819

    /// <summary>
    /// Gets the first error in the list of errors.
    /// </summary>
    public Error? FirstError => Errors[0];

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with a single error.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="error">The error associated with the result.</param>
    /// <exception cref="ArgumentNullException">Thrown when the error is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the error type is invalid for the success status.</exception>
    protected Result(bool isSuccess, Error? error)
    {
        ArgumentNullException.ThrowIfNull(error);

#pragma warning disable IDE0048
        if (isSuccess && error.Type != ErrorType.None ||
            !isSuccess && error.Type == ErrorType.None)
#pragma warning restore IDE0048
        {
            throw new ArgumentException("Invalid error type on result.");
        }

        IsSuccess = isSuccess;
        Errors = [error];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with multiple errors.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="errors">The errors associated with the result.</param>
    /// <exception cref="ArgumentException">Thrown when the error type is invalid for the success status.</exception>
    protected Result(bool isSuccess, IEnumerable<Error> errors)
    {
        Error[] arrayErrors = errors.ToArray();

#pragma warning disable IDE0048
        if (isSuccess && arrayErrors.Length != 0 ||
            !IsSuccess && arrayErrors.Length == 0)
#pragma warning restore IDE0048
        {
            throw new ArgumentException("Invalid error type on result.");
        }

        IsSuccess = isSuccess;
        Errors = arrayErrors;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with an array of errors.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="errors">The errors associated with the result.</param>
    /// <exception cref="ArgumentException">Thrown when the error type is invalid for the success status.</exception>
    protected Result(bool isSuccess, Error[] errors)
    {
#pragma warning disable CA1062
#pragma warning disable IDE0048
        if (isSuccess && errors.Length != 0 ||
            !IsSuccess && errors.Length == 0)
#pragma warning restore IDE0048
#pragma warning restore CA1062
        {
            throw new ArgumentException("Invalid error type on result.");
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful <see cref="Result"/>.</returns>
    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    /// <summary>
    /// Creates a successful result with a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value associated with the result.</param>
    /// <returns>A successful <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }

    /// <summary>
    /// Creates a failed result with a single error.
    /// </summary>
    /// <param name="error">The error associated with the result.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Failure(Error? error)
    {
        return new Result(false, error);
    }

    /// <summary>
    /// Creates a failed result with a single error and a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="error">The error associated with the result.</param>
    /// <returns>A failed <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure<TValue>(Error? error)
    {
        return new Result<TValue>(default, false, error);
    }

    /// <summary>
    /// Creates a failed result with multiple errors.
    /// </summary>
    /// <param name="errors">The errors associated with the result.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Failure(IEnumerable<Error> errors)
    {
        return new Result(false, errors);
    }

    /// <summary>
    /// Creates a failed result with multiple errors and a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="errors">The errors associated with the result.</param>
    /// <returns>A failed <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors)
    {
        return new Result<TValue>(default, false, errors);
    }

    /// <summary>
    /// Creates a failed result with an array of errors.
    /// </summary>
    /// <param name="errors">The errors associated with the result.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Failure(Error[] errors)
    {
        return new Result(false, errors);
    }

    /// <summary>
    /// Creates a failed result with an array of errors and a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="errors">The errors associated with the result.</param>
    /// <returns>A failed <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure<TValue>(Error[] errors)
    {
        return new Result<TValue>(default, false, errors);
    }
}
