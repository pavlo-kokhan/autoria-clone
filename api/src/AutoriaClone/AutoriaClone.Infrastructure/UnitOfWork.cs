using AutoriaClone.Domain;
using AutoriaClone.Domain.Aggregates.Entities.User;
using AutoriaClone.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace AutoriaClone.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;
    
    private IUserRepository? _userRepository;

    public UnitOfWork(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
    }

    public IUserRepository UserRepository
        => _userRepository ??= _serviceProvider.GetRequiredService<IUserRepository>();
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        => _dbContext.SaveChangesAsync(cancellationToken);
}