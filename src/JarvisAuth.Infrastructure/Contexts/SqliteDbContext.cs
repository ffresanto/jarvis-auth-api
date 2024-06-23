using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.models;
using JarvisAuth.Infrastructure.Mappings.JarvisMapping;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Contexts
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }

        public DbSet<GenderType> GenderTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<UserJarvis> UserJarvis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenderTypesMapping());
            modelBuilder.ApplyConfiguration(new DocumentTypesMapping());
            modelBuilder.ApplyConfiguration(new UserJarvisMapping());
        }
    }

}
