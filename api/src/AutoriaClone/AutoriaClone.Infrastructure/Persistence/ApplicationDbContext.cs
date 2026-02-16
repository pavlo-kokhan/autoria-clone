using System.Reflection;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement;
using AutoriaClone.Domain.Aggregates.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoriaClone.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CategoryEntity> Categories { get; set; }
    
    public DbSet<MakeEntity> Makes { get; set; }
    
    public DbSet<ModelEntity> Models { get; set; }
    
    public DbSet<GenerationEntity> Generations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(GetType()) ?? throw new InvalidOperationException());
    }
}