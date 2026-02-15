namespace AutoriaClone.Api.Application.Options;

public class AzureCommunicationServicesOptions
{
    public const string SectionName = nameof(AzureCommunicationServicesOptions);
    
    public string ConnectionString { get; set; } = string.Empty;
    
    public string SenderName { get; set; } = string.Empty;
    
    public string SenderEmail { get; set; } = string.Empty;
}