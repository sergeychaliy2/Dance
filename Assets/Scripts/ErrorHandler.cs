using System.Collections.Generic;
using UnityEngine;

public interface IErrorHandler
{
    void LogError(ErrorType errorType);
    void LogCustomError(ErrorType errorType, string message);
    string GetErrorMessage(ErrorType errorType);
}
public class ErrorHandler : IErrorHandler
{
    private static readonly Dictionary<ErrorType, string> errorMessages = new Dictionary<ErrorType, string>
    {
        { ErrorType.NullReference, "Null reference occurred." },
        { ErrorType.AudioClipListNull, "Null list audioclip." },
        { ErrorType.IndexOutOfRange, "Index out of range." },
        { ErrorType.InvalidArgument, "Invalid argument provided." },
        { ErrorType.OperationFailed, "Operation failed." },
        { ErrorType.UnauthorizedAccess, "Unauthorized access attempt." },
        { ErrorType.UnknownError, "An unknown error occurred." }
    };

    public void LogError(ErrorType errorType)
    {
        string errorMessage = GetErrorMessage(errorType);
        Debug.Log($"Error: {errorMessage}");
    }

    public void LogCustomError(ErrorType errorType, string message)
    {
        if (errorMessages.ContainsKey(errorType))
        {
            errorMessages[errorType] = message;
        }
        else
        {
            errorMessages.Add(errorType, message);
        }
        Debug.Log($"Error: {message}");
    }

    public string GetErrorMessage(ErrorType errorType)
    {
        if (errorMessages.TryGetValue(errorType, out string errorMessage))
        {
            return errorMessage;
        }
        else
        {
            return "Undefined error type.";
        }
    }
}
