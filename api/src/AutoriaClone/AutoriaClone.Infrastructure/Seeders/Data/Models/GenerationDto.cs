using System.Text.Json.Serialization;

namespace AutoriaClone.Infrastructure.Seeders.Data.Models;

public class GenerationDto
{
    public string Name { get; set; } = string.Empty;
        
    [JsonPropertyName("yearFrom")]
    public int? YearFrom { get; set; }
        
    [JsonPropertyName("yearTo")]
    public int? YearTo { get; set; }
}