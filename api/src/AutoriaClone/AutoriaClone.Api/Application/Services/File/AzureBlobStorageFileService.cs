using AutoriaClone.Api.Application.Constants;
using AutoriaClone.Api.Application.Options;
using AutoriaClone.Api.Application.Services.Abstract;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;

namespace AutoriaClone.Api.Application.Services.File;

public class AzureBlobStorageFileService : IFileService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly AzureBlobStorageOptions _options;

    public AzureBlobStorageFileService(
        BlobServiceClient blobServiceClient,
        IOptions<AzureBlobStorageOptions> options)
    {
        _blobServiceClient = blobServiceClient;
        _options = options.Value;
    }

    public async Task<UploadFileInfo?> UploadAsync(string key, IFormFile file, CancellationToken cancellationToken = default)
    {
        var container = _blobServiceClient.GetBlobContainerClient(_options.ImagesContainerName);
        var blobClient = container.GetBlobClient(key);
        
        // todo: maybe do a multipart upload
        await using var stream = file.OpenReadStream();
        
        var response = await blobClient.UploadAsync(
            stream,
            new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                },
                Metadata = new Dictionary<string, string>
                {
                    [BlobMetadataKeys.OriginalFileName] = file.FileName
                }
            },
            cancellationToken);

        if (!response.HasValue)
            return null;
        
        var sasUrl = CreateReadSasUrl(blobClient, TimeSpan.FromMinutes(_options.UploadSasMinutes));
        
        return new UploadFileInfo(sasUrl);
    }

    public Dictionary<string, string> GetSharedAccessSignature(IEnumerable<string> keys, CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_options.ImagesContainerName);

        var result = new Dictionary<string, string>();

        foreach (var key in keys.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct())
        {
            var blobClient = containerClient.GetBlobClient(key);
            result[key] = CreateReadSasUrl(blobClient, TimeSpan.FromMinutes(_options.ReadSasMinutes));
        }

        return result;
    }

    private static string CreateReadSasUrl(BlobClient blobClient, TimeSpan timeToLive)
    {
        if (!blobClient.CanGenerateSasUri)
            return string.Empty;

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = blobClient.BlobContainerName,
            BlobName = blobClient.Name,
            Resource = "b", // todo: remove magic literal
            ExpiresOn = DateTimeOffset.UtcNow.Add(timeToLive)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        return blobClient.GenerateSasUri(sasBuilder).ToString();
    }
}