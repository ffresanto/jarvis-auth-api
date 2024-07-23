using JarvisAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JarvisAuth.Infrastructure.Mappings.Application
{
    public class ApplicationPermissionMapping : IEntityTypeConfiguration<ApplicationPermission>
    {
        public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
        {
            builder.ToTable("applications_permissions");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(u => u.ApplicationId)
            .HasColumnName("application_id")
            .IsRequired();

            builder.Property(u => u.Name)
            .HasColumnName("name")
            .IsRequired();
        }
    }
}
