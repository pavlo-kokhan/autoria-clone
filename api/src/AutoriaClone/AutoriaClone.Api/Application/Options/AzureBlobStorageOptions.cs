namespace AutoriaClone.Api.Application.Options;

public class AzureBlobStorageOptions
{
    public const string SectionName = nameof(AzureBlobStorageOptions);

    public required string ImagesContainerName { get; set; }

    public required int UploadSasMinutes { get; set; }
    
    public required int ReadSasMinutes { get; set; }
}