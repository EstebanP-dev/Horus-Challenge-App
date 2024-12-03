using System.Diagnostics.CodeAnalysis;

namespace Mobile.Primitives;

/// <summary>
/// Represents the result of an operation with a value, containing success status and errors if any.
/// </summary>
/// <typeparam name="TValue">The type of the value associated with the result.</typeparam>
public sealed class Result<TValue> : Result
{
    private readonly TValue? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class with a single error.
    /// </summary>
    /// <param name="value">The value associated with the result.</param>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="error">The error associated with the result.</param>
    public Result(
            TValue? value,
            bool isSuccess,
            Error? error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class with multiple errors.
    /// </summary>
    /// <param name="value">The value associated with the result.</param>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="errors">The errors associated with the result.</param>
    public Result(
            TValue? value,
            bool isSuccess,
            IEnumerable<Error> errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class with an array of errors.
    /// </summary>
    /// <param name="value">The value associated with the result.</param>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="errors">The errors associated with the result.</param>
    public Result(
            TValue? value,
            bool isSuccess,
            Error[] errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the value associated with the result.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the value of a failure result is accessed.</exception>
    [NotNull]
    public TValue Value => _value is not null
        ? _value
#pragma warning disable IDE0055
        : throw new InvalidOperationException("The value of a failure result cannot be accessed.");
#pragma warning restore IDE0055

    /// <summary>
    /// Implicitly converts a value to a <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A successful <see cref="Result{TValue}"/> if the value is not null; otherwise, a failed <see cref="Result{TValue}"/> with a null value error.</returns>
#pragma warning disable CA2225
    public static implicit operator Result<TValue>(TValue? value) => value is not null
        ? Success(value)
        : Failure<TValue>(Error.NullValue);
}
