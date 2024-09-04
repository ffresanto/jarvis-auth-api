using JarvisAuth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JarvisAuth.Infrastructure.Mappings.User
{
    public class UserPermissionMapping : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("users_permissions");

            builder.HasKey(u => new { u.UserId, u.ApplicationPermissionId });

            builder.Property(u => u.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(u => u.ApplicationPermissionId)
                .HasColumnName("application_permission_id")
                .IsRequired();
        }
    }
}
