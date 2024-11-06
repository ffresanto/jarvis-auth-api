using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Services.Log;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace JarvisAuth.API.Handlers
{
    public class GlobalExceptionHandler(ILogService logService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionResponse = new Response<string[]>
            {
                StatusCode = 500,
                Success = false,
                Message = GlobalMessages.GLOBAL_EXCEPTION_500,
                Data = [],
                Errors = [exception.Message]
            };

            var errorMessage = JsonSerializer.Serialize(exceptionResponse);

            logService.LogError(errorMessage);

            httpContext.Response.StatusCode = 500;

            await httpContext.Response
                .WriteAsJsonAsync(exceptionResponse, cancellationToken);

            return true;
        }
    }
}
