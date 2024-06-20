using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Infrastructure.Configurations.Contexts
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }
    }

    public static class SqliteDbContextExtension
    {
        public static IServiceCollection AddSqliteDbContextAndHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var dataSource = "Data Source=jarvis.db";

            services.AddDbContext<SqliteDbContext>(options => options.UseSqlite(dataSource));

            services.AddHealthChecks().AddSqlite(dataSource, name: "sqlite");

            return services;
        }
    }
}
