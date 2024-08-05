using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Mappings.Application;
using JarvisAuth.Infrastructure.Mappings.User;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Contexts
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<UserLinkedApplication> UserLinkedApplications { get; set; }
        public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        public DbSet<UserPermission> UsersPermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new ApplicationMapping());
            modelBuilder.ApplyConfiguration(new UserLinkedApplicationMapping());
            modelBuilder.ApplyConfiguration(new ApplicationPermissionMapping());
            modelBuilder.ApplyConfiguration(new UserPermissionMapping());
        }
    }
}
