namespace AutoriaClone.Api.Application.Options;

public class NovaPoshtaOptions
{
    public const string SectionName = nameof(NovaPoshtaOptions);
    
    public string ApiKey { get; set; } = string.Empty;
    
    public string BaseUrl { get; set; } = string.Empty;
}