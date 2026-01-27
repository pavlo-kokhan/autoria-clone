namespace AutoriaClone.Domain.Aggregates.Entities.User;

public interface IUserRepository
{
    Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<UserEntity?> GetByRefreshTokenAsync(string token, CancellationToken cancellationToken = default);
}
