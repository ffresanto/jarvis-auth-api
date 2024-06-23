using JarvisAuth.Application.Services;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Application.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IJarvisService, JarvisService>();

            return services;
        }
    }
}
