using AutoriaClone.Domain.Aggregates.Entities.User;
using AutoriaClone.Domain.Aggregates.ValueObjects.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoriaClone.Infrastructure.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.OwnsMany(u => u.RefreshTokens, rt =>
        {
            rt.ToTable("RefreshTokens");
            rt.WithOwner().HasForeignKey("UserId");
            rt.Property<Guid>("Id");
            rt.HasKey("Id");

            rt.Property(r => r.Token).IsRequired();
            rt.Property(r => r.ExpiresAt).IsRequired();
        });
        
        builder.OwnsOne<AddressValueObject>(u => u.Address, rt =>
        {
            rt.ToTable("UserAddresses");
            rt.WithOwner().HasForeignKey("UserId");
            rt.Property<int>("Id");
            rt.HasKey("Id");
        });
    }
}