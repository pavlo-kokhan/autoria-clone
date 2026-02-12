namespace AutoriaClone.Api.Application.Options;

public class AzureStorageOptions
{
    public const string SectionName = nameof(AzureStorageOptions);

    public required string ContainerName { get; set; }
    
    public required int UploadSasMinutes { get; set; }
    
    public required int ReadSasMinutes { get; set; }
}