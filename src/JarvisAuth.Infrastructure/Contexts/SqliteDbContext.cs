using JarvisAuth.Domain.Entities;
using JarvisAuth.Infrastructure.Mappings.JarvisMapping;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Contexts
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }
        public DbSet<UserJarvis> UserJarvis { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserJarvisMapping());
        }
    }

}
