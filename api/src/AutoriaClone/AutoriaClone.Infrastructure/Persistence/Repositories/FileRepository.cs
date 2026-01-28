using AutoriaClone.Domain.Aggregates.Entities.File;

namespace AutoriaClone.Infrastructure.Persistence.Repositories;

public class FileRepository : IFileRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FileRepository(ApplicationDbContext dbContext) 
        => _dbContext = dbContext;

    public async Task CreateAsync(FileEntity file, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(file, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}