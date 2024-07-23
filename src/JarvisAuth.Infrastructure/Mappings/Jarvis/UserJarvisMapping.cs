using JarvisAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JarvisAuth.Infrastructure.Mappings.Jarvis
{
    public class UserJarvisMapping : IEntityTypeConfiguration<UserJarvis>
    {
        public void Configure(EntityTypeBuilder<UserJarvis> builder)
        {
            builder.ToTable("users_jarvis");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            builder.Property(u => u.IsAdmin)
                .HasColumnName("is_admin")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(u => u.Enabled)
                .HasColumnName("enabled")
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
