using UnityEngine;

public static class DebugExtensions
{
    private static IErrorHandler errorHandler = new ErrorHandler();

    public static void Log(this ErrorType errorType)
    {
        errorHandler.LogError(errorType);
    }

    public static void LogCustomError(this ErrorType errorType, string message)
    {
        errorHandler.LogCustomError(errorType, message);
    }
}
