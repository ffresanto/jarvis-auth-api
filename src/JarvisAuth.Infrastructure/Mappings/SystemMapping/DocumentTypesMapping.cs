using JarvisAuth.Domain.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JarvisAuth.Infrastructure.Mappings.TypesMapping
{
    public class DocumentTypesMapping : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.ToTable("documents_types");

            builder.HasKey(e => e.id);

            builder.Property(e => e.name)
                .HasMaxLength(20)
                .HasColumnName("name");
        }
    }
}
