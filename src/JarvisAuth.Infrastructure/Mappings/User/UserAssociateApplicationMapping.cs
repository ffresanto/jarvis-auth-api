using JarvisAuth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JarvisAuth.Infrastructure.Mappings.User
{
    public class UserAssociateApplicationMapping : IEntityTypeConfiguration<UserAssociateApplication>
    {
        public void Configure(EntityTypeBuilder<UserAssociateApplication> builder)
        {
            builder.ToTable("users_associate_applications");

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
