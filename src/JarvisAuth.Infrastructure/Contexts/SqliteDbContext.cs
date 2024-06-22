using JarvisAuth.Domain.models;
using JarvisAuth.Infrastructure.Mappings.TypesMapping;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenderTypesMapping());
            modelBuilder.ApplyConfiguration(new DocumentTypesMapping());
        }
    }

}
