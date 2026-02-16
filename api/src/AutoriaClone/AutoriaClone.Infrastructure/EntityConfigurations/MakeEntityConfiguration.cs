using AutoriaClone.Domain.Aggregates.Entities.Advertisement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoriaClone.Infrastructure.EntityConfigurations;

public class MakeEntityConfiguration : IEntityTypeConfiguration<MakeEntity>
{
    public void Configure(EntityTypeBuilder<MakeEntity> builder)
    {
        builder.ToTable("Makes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        
        builder.HasIndex(x => x.CategoryId);

        builder.HasMany(x => x.Models)
            .WithOne()
            .HasForeignKey(x => x.MakeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}