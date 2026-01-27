using AutoriaClone.Domain.Aggregates.Entities.User;
using AutoriaClone.Domain.Constants;
using AutoriaClone.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoriaClone.Infrastructure;

public sealed class DatabaseSeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public DatabaseSeeder(ApplicationDbContext dbContext, UserManager<UserEntity> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(cancellationToken);
        await SeedUserAsync(cancellationToken);
    }

    private async Task SeedRolesAsync(CancellationToken cancellationToken = default)
    {
        if (!await _roleManager.Roles.AnyAsync(cancellationToken: cancellationToken))
        {
            foreach (var role in Enum.GetValues<Role>())
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(role.ToString()));
            }
        }
    }

    private async Task SeedUserAsync(CancellationToken cancellationToken = default)
    {
        if (!await _dbContext.Set<UserEntity>().AnyAsync(cancellationToken))
        {
            var user = new UserEntity { UserName = "user@gmail.com", Email = "user@gmail.com" };

            await _userManager.CreateAsync(user, "SuperPassword");
            await _userManager.AddToRoleAsync(user, nameof(Role.User));
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}