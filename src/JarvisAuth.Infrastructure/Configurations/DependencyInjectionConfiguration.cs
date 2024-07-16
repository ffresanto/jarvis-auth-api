using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Infrastructure.Repositories;
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

            return services;
        }
    }
}
