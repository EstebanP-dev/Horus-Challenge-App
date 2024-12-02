using System.Net;
using Microsoft.Extensions.Logging;
using Refit;

namespace Mobile.Behaviors;

/// <summary>
/// Pipeline behavior for handling exceptions in the request processing pipeline.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
        ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    /// <summary>
    /// Handles the request and processes any exceptions that occur.
    /// </summary>
    /// <param name="request">The request object.</param>
    /// <param name="next">The delegate to invoke the next behavior in the pipeline.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response object.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next()
                .ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            return ResponseFromStatusCode(ex.StatusCode, ex.Message);
        }
        catch (ApiException ex)
        {
            return ResponseFromStatusCode(ex.StatusCode, ex.Message);
        }
        catch (NoInternetConnectionException ex)
        {
#pragma warning disable IDE0008
            var error = Error.Failure(nameof(ExceptionHandlingPipelineBehavior<TRequest, TResponse>), ex.Message);
#pragma warning restore IDE0008

            return ToTResponse(error);
        }
#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
            logger.LogException(request.GetType().Name, ex);

            return ToTResponse(Error.Unexpected(nameof(ExceptionHandlingPipelineBehavior<TRequest, TResponse>), "Unexpected error. Try again later."));
        }
    }

    /// <summary>
    /// Creates a response from the given HTTP status code and message.
    /// </summary>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>The response object.</returns>
    private static TResponse ResponseFromStatusCode(HttpStatusCode? statusCode, string message)
    {
#pragma warning disable IDE0072 // Add missing cases
        Error error = statusCode switch
        {
            HttpStatusCode.BadRequest => Error.Failure(nameof(ExceptionHandlingPipelineBehavior<TRequest, TResponse>), message),
            HttpStatusCode.Unauthorized => Error.Failure(nameof(ExceptionHandlingPipelineBehavior<TRequest, TResponse>), message),
            HttpStatusCode.Forbidden => Error.Failure(nameof(ExceptionHandlingPipelineBehavior<TRequest, TResponse>), "You are not able to access to this information."),
            HttpStatusCode.NotFound => Error.NotFound(nameof(ExceptionHandlingPipelineBehavior<TRequest, TResponse>), message),
            _ => Error.Unexpected(nameof(ExceptionHandlingPipelineBehavior<TRequest, TResponse>), "Unexpected error. Try again later.")
        };
#pragma warning restore IDE0072 // Add missing cases

        return ToTResponse(error);
    }

    /// <summary>
    /// Converts a single error to a response object.
    /// </summary>
    /// <param name="error">The error object.</param>
    /// <returns>The response object.</returns>
    private static TResponse ToTResponse(Error error)
    {
        return ToTResponse([error]);
    }

    /// <summary>
    /// Converts a collection of errors to a response object.
    /// </summary>
    /// <param name="errors">The collection of errors.</param>
    /// <returns>The response object.</returns>
    private static TResponse ToTResponse(IEnumerable<Error> errors)
    {
        bool isGenericType = typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>);

        if (isGenericType)
        {
            Type currentType = typeof(TResponse);
            Type? valueType = currentType.GetGenericArguments().FirstOrDefault();

            MethodInfo[] methods = typeof(Result)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(m => m is { Name: "Failure", IsGenericMethodDefinition: true })
                .ToArray();

            MethodInfo methodInfo = FindFailureMethod(methods, typeof(IEnumerable<Error>))
                                    ?? throw new InvalidOperationException(
                                        "The appropriate Failure method could not be found.");
            MethodInfo genericMethod = methodInfo.MakeGenericMethod(valueType!);
            return (TResponse)genericMethod.Invoke(null, [errors])!;
        }
        else
        {
            MethodInfo[] methods = typeof(Result)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(m => m is { Name: "Failure", IsGenericMethodDefinition: false })
                .ToArray();

            MethodInfo? methodInfo = FindFailureMethod(methods, typeof(IEnumerable<Error>));

            return methodInfo is null
                ? throw new InvalidOperationException("The appropriate Failure method could not be found.")
                : (TResponse)methodInfo.Invoke(null, [errors])!;
        }
    }

    /// <summary>
    /// Finds the appropriate Failure method from the given methods.
    /// </summary>
    /// <param name="methods">The array of methods to search.</param>
    /// <param name="parameterType">The type of the parameter.</param>
    /// <returns>The matching method info, or null if not found.</returns>
    private static MethodInfo? FindFailureMethod(MethodInfo[] methods, Type parameterType)
    {
        return Array
    .Find(
        methods,
        m => (m.ReturnType == typeof(Result) ||
#pragma warning disable IDE0048
              m.ReturnType.IsGenericType && m.ReturnType.GetGenericTypeDefinition() == typeof(Result<>)) &&
#pragma warning restore IDE0048
             m.GetParameters().Length == 1 &&
             m.GetParameters()[0].ParameterType == parameterType);
    }
}
