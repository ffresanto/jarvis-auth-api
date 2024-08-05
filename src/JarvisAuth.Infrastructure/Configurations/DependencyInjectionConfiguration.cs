using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Infrastructure.Repositories.Application;
using JarvisAuth.Infrastructure.Repositories.User;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Infrastructure.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RepositoriesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IUserLinkedApplicationRepository, UserLinkedApplicationRepositoy>();
            services.AddScoped<IApplicationPermissionRepository, ApplicationPermissionRepository>();
            services.AddScoped<IUserPermissionRepository, UserPermissionRepositoy>();

            return services;
        }
    }
}
