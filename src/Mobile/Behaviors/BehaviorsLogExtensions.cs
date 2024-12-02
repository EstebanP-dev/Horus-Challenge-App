using Microsoft.Extensions.Logging;

namespace Mobile.Behaviors;


/// <summary>
/// Provides extension methods for logging behaviors.
/// </summary>
internal static class BehaviorsLogExtensions
{
    /// <summary>
    /// Defines a logging scope for unhandled exceptions.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> _loggingScope = LoggerMessage
        .Define<string>(
            LogLevel.Error,
            new EventId(0, "ExceptionHandlingPipelineBehaviorGeneralException"),
            "Unhandled exception for {RequestName}");

    /// <summary>
    /// Logs an unhandled exception for a given request name.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="requestName">The name of the request.</param>
    /// <param name="exception">The exception to log, if any.</param>
    internal static void LogException(this ILogger logger, string requestName, Exception? exception = null)
    {
        _loggingScope(logger, requestName, exception);
    }
}
