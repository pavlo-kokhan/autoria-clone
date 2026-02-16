using AutoriaClone.Domain.Aggregates.Entities.Advertisement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoriaClone.Infrastructure.EntityConfigurations;

public class GenerationEntityConfiguration : IEntityTypeConfiguration<GenerationEntity>
{
    public void Configure(EntityTypeBuilder<GenerationEntity> builder)
    {
        builder.ToTable("Generations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.HasIndex(x => x.ModelId);

        builder.Property(x => x.YearFrom).IsRequired();
        builder.Property(x => x.YearTo).IsRequired(false);
    }
}