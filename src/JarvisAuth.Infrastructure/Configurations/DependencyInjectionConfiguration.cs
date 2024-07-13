using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Infrastructure.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RepositoriesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserJarvisRepository, UserJarvisRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();

            return services;
        }
    }
}
