using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection RepositoriesDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITypesRepository, TypesRepository>();

            return services;
        }
    }
}
