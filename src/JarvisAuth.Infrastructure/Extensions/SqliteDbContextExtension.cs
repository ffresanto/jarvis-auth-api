using JarvisAuth.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Infrastructure.Extensions
{
    public static class SqliteDbContextExtension
    {
        public static IServiceCollection AddSqliteDbContext(this IServiceCollection services, string connectionStringSqlite)
        {
            services.AddDbContext<SqliteDbContext>(options => options.UseSqlite(connectionStringSqlite));

            return services;
        }
    }
}
