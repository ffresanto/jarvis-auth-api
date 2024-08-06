using JarvisAuth.Application.Services.Application;
using JarvisAuth.Application.Services.Authentication;
using JarvisAuth.Application.Services.User;
using JarvisAuth.Domain.Interfaces.Services.Application;
using JarvisAuth.Domain.Interfaces.Services.Authentication;
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
            services.AddScoped<IUserPermissionService, UserPermissionService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
