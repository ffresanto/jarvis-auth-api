using JarvisAuth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JarvisAuth.Infrastructure.Mappings
{
    public class UserJarvisProfileApplicationMapping : IEntityTypeConfiguration<UserJarvisProfileApplication>
    {
        public void Configure(EntityTypeBuilder<UserJarvisProfileApplication> builder)
        {
            builder.ToTable("users_jarvis_profile_applications");

            builder.HasKey(u => u.UserJarvisId);

            builder.Property(u => u.UserJarvisId)
                .HasColumnName("user_jarvis_id")
                .IsRequired();

            builder.Property(u => u.ApplicationId)
                .HasColumnName("application_id")
                .IsRequired();
        }
    }
}
