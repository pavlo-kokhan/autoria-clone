using AutoriaClone.Api.Application.Options;
using AutoriaClone.Api.Application.Services.Abstract;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;

namespace AutoriaClone.Api.Application.Services.File;

public class AzureStorageService : IStorageService
{
    private readonly BlobContainerClient _containerClient;
    private readonly AzureStorageOptions _azureStorageOptions;

    public AzureStorageService(BlobServiceClient blobServiceClient, IOptions<AzureStorageOptions> azureStorageOptions)
    {
        _azureStorageOptions = azureStorageOptions.Value;
        _containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageOptions.ContainerName);
    }

    public string GenerateWriteSasUrl(string blobKey, string contentType)
    {
        var blobClient = _containerClient.GetBlobClient(blobKey);

        return GetSasUrl(
            blobClient,
            TimeSpan.FromMinutes(_azureStorageOptions.UploadSasMinutes),
            BlobSasPermissions.Write | BlobSasPermissions.Create,
            contentType);
    }
    
    public Dictionary<string, string> GetReadSasUrls(IEnumerable<string> blobKeys)
    {
        var urls = new Dictionary<string, string>();

        foreach (var key in blobKeys.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct())
        {
            var blobClient = _containerClient.GetBlobClient(key);
            
            urls[key] = GetSasUrl(
                blobClient,
                TimeSpan.FromMinutes(_azureStorageOptions.ReadSasMinutes));
        }

        return urls;
    }

    private static string GetSasUrl(
        BlobClient blobClient,
        TimeSpan timeToLive,
        BlobSasPermissions permissions = BlobSasPermissions.Read,
        string? contentType = null)
    {
        if (!blobClient.CanGenerateSasUri)
            return string.Empty;

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = blobClient.BlobContainerName,
            BlobName = blobClient.Name,
            Resource = "b",
            ExpiresOn = DateTimeOffset.UtcNow.Add(timeToLive)
        };

        if (!string.IsNullOrEmpty(contentType))
            sasBuilder.ContentType = contentType;

        sasBuilder.SetPermissions(permissions);

        return blobClient.GenerateSasUri(sasBuilder).ToString();
    }
}