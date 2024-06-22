using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Infrastructure.Configurations
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddSqliteHealthCheck(this IServiceCollection services, string connectionStringSqlite)
        {
            services.AddHealthChecks().AddSqlite(connectionStringSqlite, name: "sqlite");

            return services;
        }
    }
}
