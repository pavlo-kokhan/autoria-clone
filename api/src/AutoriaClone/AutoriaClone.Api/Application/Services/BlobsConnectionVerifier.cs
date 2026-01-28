using AutoriaClone.Api.Application.Options;
using AutoriaClone.Api.Application.Services.Abstract;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace AutoriaClone.Api.Application.Services;

public class BlobsConnectionVerifier : IBlobsConnectionVerifier
{
    private readonly ILogger<BlobsConnectionVerifier> _logger;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly AzureBlobStorageOptions _blobStorageOptions;

    public BlobsConnectionVerifier(
        ILogger<BlobsConnectionVerifier> logger, 
        BlobServiceClient blobServiceClient, 
        IOptions<AzureBlobStorageOptions> blobStorageOptions)
    {
        _logger = logger;
        _blobServiceClient = blobServiceClient;
        _blobStorageOptions = blobStorageOptions.Value;
    }

    public async Task CheckConnectionAsync()
    {
        try
        {
            await _blobServiceClient.GetPropertiesAsync();
            _logger.LogInformation("Connection to Azure Blob Storage is successful.");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed to connect to Azure Blob Storage.");
            throw;
        }
    }

    public async Task EnsureContainersExistsAsync()
    {
        await EnsureImagesContainerExistsAsync();
    }
    
    private async Task EnsureImagesContainerExistsAsync()
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_blobStorageOptions.ImagesContainerName);
        
        if (await containerClient.ExistsAsync())
            return;
        
        await containerClient.CreateAsync();
        
        _logger.LogInformation("Images container created.");
    }
}