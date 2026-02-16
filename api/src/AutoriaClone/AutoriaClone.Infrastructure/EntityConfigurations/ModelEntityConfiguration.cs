using AutoriaClone.Domain.Aggregates.Entities.Advertisement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoriaClone.Infrastructure.EntityConfigurations;

public class ModelEntityConfiguration : IEntityTypeConfiguration<ModelEntity>
{
    public void Configure(EntityTypeBuilder<ModelEntity> builder)
    {
        builder.ToTable("Models");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.HasIndex(x => x.MakeId);

        builder.HasMany(x => x.Generations)
            .WithOne()
            .HasForeignKey(x => x.ModelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}