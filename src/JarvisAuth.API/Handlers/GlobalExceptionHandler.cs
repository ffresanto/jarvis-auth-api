using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Responses.Shared;
using Microsoft.AspNetCore.Diagnostics;

namespace JarvisAuth.API.Handlers
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var logData = new
            {
                Message = exception.Message,
                Time = DateTime.UtcNow
            };

            logger.LogError(
            exception, "Exception occurred: {logData}", logData);

            var exceptionResponse = new Response<string[]>
            {
                StatusCode = 500,
                Success = false,
                Message = GlobalMessages.GLOBAL_EXCEPTION,
                Data = [],
                Errors = [exception.Message]
            };

            httpContext.Response.StatusCode = 500;

            await httpContext.Response
                .WriteAsJsonAsync(exceptionResponse, cancellationToken);

            return true;
        }
    }
}
