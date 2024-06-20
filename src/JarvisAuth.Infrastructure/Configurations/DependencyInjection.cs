using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection RepositoriesDependencies(this IServiceCollection services)
        {
            return services;
        }
    }
}
