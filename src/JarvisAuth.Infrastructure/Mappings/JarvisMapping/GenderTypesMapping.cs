using JarvisAuth.Domain.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JarvisAuth.Infrastructure.Mappings.JarvisMapping
{
    public class GenderTypesMapping : IEntityTypeConfiguration<GenderType>
    {
        public void Configure(EntityTypeBuilder<GenderType> builder)
        {
            builder.ToTable("genders_types");

            builder.HasKey(e => e.id);

            builder.Property(e => e.name)
            .HasMaxLength(10)
            .HasColumnName("name");
        }
    }
}
