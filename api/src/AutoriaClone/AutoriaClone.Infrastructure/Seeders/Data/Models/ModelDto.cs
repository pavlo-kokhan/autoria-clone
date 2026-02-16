namespace AutoriaClone.Infrastructure.Seeders.Data.Models;

public class ModelDto
{
    public string Name { get; set; } = string.Empty;
    
    public List<GenerationDto> Generations { get; set; } = new();
}