namespace AutoriaClone.Api.Application.Services.Abstract;

public interface IStorageService
{
    public string GenerateWriteSasUrl(string blobKey, string contentType);

    public Dictionary<string, string> GetReadSasUrls(IEnumerable<string> blobKeys);
}