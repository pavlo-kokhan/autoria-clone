using AutoriaClone.Domain.Aggregates.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace AutoriaClone.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
        => _dbContext = dbContext;

    public Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => _dbContext
            .Set<UserEntity>()
            .FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper(), cancellationToken);

    public Task<UserEntity?> GetByIdAsync(int userId, CancellationToken cancellationToken = default) 
        => _dbContext
            .Set<UserEntity>()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

    public Task<UserEntity?> GetByRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
        => _dbContext
            .Set<UserEntity>()
            .Include(u => u.RefreshTokens.Where(rt => rt.Token == token))
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == token), cancellationToken);
}
