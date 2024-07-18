using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Domain.Interfaces.Repositories.Jarvis;
using JarvisAuth.Infrastructure.Repositories.Application;
using JarvisAuth.Infrastructure.Repositories.Jarvis;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Infrastructure.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RepositoriesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IJarvisRepository, JarvisRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IUserJarvisLinkedApplicationRepository, UserJarvisLinkedApplicationRepositoy>();
            services.AddScoped<IApplicationPermissionRepository, ApplicationPermissionRepository>();

            return services;
        }
    }
}
