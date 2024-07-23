using JarvisAuth.Application.Services;
using JarvisAuth.Domain.Interfaces.Services.Application;
using JarvisAuth.Domain.Interfaces.Services.Jarvis;
using JarvisAuth.Domain.Interfaces.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Application.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IJarvisService, JarvisService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
