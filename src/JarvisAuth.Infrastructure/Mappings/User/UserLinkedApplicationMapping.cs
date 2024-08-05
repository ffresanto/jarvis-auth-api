using JarvisAuth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JarvisAuth.Infrastructure.Mappings.User
{
    public class UserLinkedApplicationMapping : IEntityTypeConfiguration<UserLinkedApplication>
    {
        public void Configure(EntityTypeBuilder<UserLinkedApplication> builder)
        {
            builder.ToTable("users_linked_applications");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(u => u.ApplicationId)
                .HasColumnName("application_id")
                .IsRequired();
        }
    }
}
