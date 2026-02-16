namespace AutoriaClone.Infrastructure.Seeders.Data.Models;

public class BrandDto
{
    public string Brand { get; set; } = string.Empty;
    
    public List<ModelDto> Models { get; set; } = new();
}