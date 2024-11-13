using JarvisAuth.Domain.Interfaces.Services.Logger;
using Microsoft.Extensions.Logging;

namespace JarvisAuth.Application.Services.Logger
{
    public class LogService(ILogger<LogService> logger) : ILogService
    {
        public void LogInformation(string message)
        {
            logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            logger.LogWarning(message);
        }

        public void LogError(string message, Exception exception = null)
        {
            if (exception != null)
            {
                logger.LogError(exception, message);
            }
            else
            {
                logger.LogError(message);
            }
        }
    }
}
