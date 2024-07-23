using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JarvisAuth.Infrastructure.Mappings.Application
{
    public class ApplicationMapping : IEntityTypeConfiguration<Domain.Entities.Application>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Application> builder)
        {
            builder.ToTable("applications");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(u => u.Name)
                   .HasColumnName("name")
                   .IsRequired();

            builder.Property(u => u.Enabled)
                   .HasColumnName("enabled")
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .HasColumnName("created_at")
                   .IsRequired();

            builder.Property(u => u.UpdatedAt)
                   .HasColumnName("updated_at")
                   .IsRequired();
        }
    }
}
