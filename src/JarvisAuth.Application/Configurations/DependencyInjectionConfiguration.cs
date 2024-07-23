using JarvisAuth.Application.Services;
using JarvisAuth.Domain.Interfaces.Services.Application;
using JarvisAuth.Domain.Interfaces.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Application.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicationService, ApplicationService>();

            return services;
        }
    }
}
