using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api;

public class GlobalExceptionHandler() : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCodes, errorMessage) = exception switch
        {
            NotFoundException => (404, exception.Message),
            _ => (500, "Something went wrong")
        };

        httpContext.Response.StatusCode = statusCodes;
        await httpContext.Response.WriteAsJsonAsync(errorMessage, cancellationToken);
        return true;
    }
}
