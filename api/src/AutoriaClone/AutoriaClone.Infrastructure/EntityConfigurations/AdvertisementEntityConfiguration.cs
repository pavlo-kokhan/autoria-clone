using AutoriaClone.Domain.Aggregates.Entities.Advertisement.Root;
using AutoriaClone.Domain.Aggregates.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoriaClone.Infrastructure.EntityConfigurations;

public class AdvertisementEntityConfiguration : IEntityTypeConfiguration<AdvertisementEntity>
{
    public void Configure(EntityTypeBuilder<AdvertisementEntity> builder)
    {
        builder.ToTable("Advertisements");
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RegionRef);

        // ImageIds
        builder
            .Property(x => x.ImageIds)
            .HasColumnType("jsonb")
            .HasDefaultValue(Array.Empty<int>());

        // UserEntity
        builder
            .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        // PriceValueObject
        builder
            .OwnsOne(
                x => x.LastPrice,
                price =>
                {
                    price.ToTable("AdvertisementPrices");
                    price.WithOwner().HasForeignKey("AdvertisementId");
                    price.Property<int>("Id");
                    price.HasKey("Id");
                    price.HasIndex(x => x.Value);
                });
        
        // VehicleDetailsValueObject
        builder
            .OwnsOne(
                x => x.VehicleDetails,
                details =>
                {
                    details.ToTable("VehicleDetails");
                    details.WithOwner().HasForeignKey("AdvertisementId");
                    details.Property<int>("Id");
                    details.HasKey("Id");
                    
                    // flags
                    details.Property(d => d.ComfortOptions).HasConversion<long>();
                    details.Property(d => d.SafetyOptions).HasConversion<long>();
                    details.Property(d => d.MultimediaOptions).HasConversion<long>();
                    
                    // PowerValueObject
                    // details.OwnsOne(d => d.Power, power =>
                    // {
                    //     power.Property(p => p.Value).HasColumnName("PowerValue");
                    //     power.Property(p => p.Unit).HasColumnName("PowerUnit");
                    // });
                    details.Property(d => d.Power).HasColumnType("jsonb");
                    
                    // FuelConsumptionValueObject
                    // details.OwnsOne(d => d.FuelConsumption, fuel =>
                    // {
                    //     fuel.Property(f => f.InCity).HasColumnName("FuelCity");
                    //     fuel.Property(f => f.InHighway).HasColumnName("FuelHighway");
                    //     fuel.Property(f => f.Combined).HasColumnName("FuelMixed");
                    // });
                    details.Property(d => d.FuelConsumption).HasColumnType("jsonb");
                });
    }
}