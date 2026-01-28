using AutoriaClone.Api.Application.Services.File;

namespace AutoriaClone.Api.Application.Services.Abstract;

public interface IFileService
{
    Task<UploadFileInfo?> UploadAsync(string key, IFormFile file, CancellationToken cancellationToken = default);
    
    Dictionary<string, string> GetSharedAccessSignature(IEnumerable<string> keys, CancellationToken cancellationToken = default);
}