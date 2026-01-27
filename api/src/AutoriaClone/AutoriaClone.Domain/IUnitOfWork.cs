using AutoriaClone.Domain.Aggregates.Entities.User;

namespace AutoriaClone.Domain;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
