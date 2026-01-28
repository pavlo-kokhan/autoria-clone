using AutoriaClone.Domain.Aggregates.Entities.File;
using AutoriaClone.Domain.Aggregates.Entities.User;

namespace AutoriaClone.Domain;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    
    IFileRepository FileRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
