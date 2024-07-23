using JarvisAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Mappings.Jarvis;
using JarvisAuth.Infrastructure.Mappings.Application;
using JarvisAuth.Infrastructure.Mappings.User;

namespace JarvisAuth.Infrastructure.Contexts
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }
        public DbSet<UserJarvis> UserJarvis { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<UserJarvisLinkedApplication> UserJarvisLinkedApplications { get; set; }
        public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserJarvisMapping());
            modelBuilder.ApplyConfiguration(new ApplicationMapping());
            modelBuilder.ApplyConfiguration(new UserJarvisLinkedApplicationMapping());
            modelBuilder.ApplyConfiguration(new ApplicationPermissionMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}
