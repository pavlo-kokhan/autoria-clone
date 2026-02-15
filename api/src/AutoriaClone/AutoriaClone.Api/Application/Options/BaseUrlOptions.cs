namespace AutoriaClone.Api.Application.Options;

public class BaseUrlOptions
{
    public const string SectionName = nameof(BaseUrlOptions);
    
    public string ClientBaseUrl { get; set; } = string.Empty;
}