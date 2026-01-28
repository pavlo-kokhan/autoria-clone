namespace AutoriaClone.Domain.Aggregates.Entities.File;

public interface IFileRepository
{
    Task CreateAsync(FileEntity file, CancellationToken cancellationToken = default);
}
