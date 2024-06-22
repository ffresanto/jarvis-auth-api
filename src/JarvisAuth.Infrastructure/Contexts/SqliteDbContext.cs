using JarvisAuth.Domain.models;
using JarvisAuth.Infrastructure.Mappings.TypesMapper;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Contexts
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }

        public DbSet<GenderType> GendersTypes { get; set; }
        public DbSet<DocumentType> DocumentsTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GendersTypesMapping());
            modelBuilder.ApplyConfiguration(new DocumentsTypesMapping());
        }
    }

}
