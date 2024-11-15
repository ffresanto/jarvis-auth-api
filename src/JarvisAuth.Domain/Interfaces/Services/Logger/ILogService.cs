namespace JarvisAuth.Domain.Interfaces.Services.Logger
{
    public interface ILogService
    {
        public void LogInformation(string message);
        public void LogWarning(string message);
        public void LogError(string message, Exception exception = null);
    }
}
