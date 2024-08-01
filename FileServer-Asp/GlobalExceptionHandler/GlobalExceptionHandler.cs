using FileServer_Asp.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace FileServer_Asp.GlobalExceptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int statusCode, string errorMessage) = exception switch
        {
            ExistingAssignException => (StatusCodes.Status409Conflict, "There is already another assign with given secret, provide unique secret."),
            _ => (500, "Error occured, Try again.")
        };

        Log.Error(errorMessage);
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorMessage);
        return true;
    }
}
