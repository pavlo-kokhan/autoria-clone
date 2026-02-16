using System.Text.Json;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement;
using AutoriaClone.Infrastructure.Persistence;
using AutoriaClone.Infrastructure.Seeders.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoriaClone.Infrastructure.Seeders;

public class VehicleSeeder
{
    private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data", "car-brands.json");
    
    private readonly ApplicationDbContext _dbContext;

    public VehicleSeeder(ApplicationDbContext dbContext) 
        => _dbContext = dbContext;
    
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Categories.AnyAsync(cancellationToken))
            return;
        
        if (!File.Exists(FilePath))
            throw new FileNotFoundException($"Seed data file not found at: {FilePath}.");

        var jsonContent = await File.ReadAllTextAsync(FilePath, cancellationToken);
        var brandsData = JsonSerializer.Deserialize<List<BrandDto>>(jsonContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (brandsData == null || !brandsData.Any())
            return;

        var category = await GetOrCreateCategoryAsync("Легкові", cancellationToken);

        foreach (var brandDto in brandsData)
        {
            var makeEntity = await _dbContext.Makes
                .FirstOrDefaultAsync(m => m.Name == brandDto.Brand && m.CategoryId == category.Id, cancellationToken);

            if (makeEntity == null)
            {
                var result = MakeEntity.Create(brandDto.Brand, category.Id);
                
                if (result.IsFailure) 
                    continue;

                makeEntity = result.Data;
                _dbContext.Makes.Add(makeEntity);
                
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            if (brandDto.Models == null) 
                continue;

            foreach (var modelDto in brandDto.Models)
            {
                var modelEntity = await _dbContext.Models
                    .FirstOrDefaultAsync(m => m.Name == modelDto.Name && m.MakeId == makeEntity.Id, cancellationToken);

                if (modelEntity == null)
                {
                    var result = ModelEntity.Create(modelDto.Name, makeEntity.Id);
                    if (result.IsFailure) continue;

                    modelEntity = result.Data;
                    _dbContext.Models.Add(modelEntity);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                if (modelDto.Generations == null) continue;

                var existingGenerations = await _dbContext.Generations
                    .Where(g => g.ModelId == modelEntity.Id)
                    .Select(g => g.Name)
                    .ToListAsync(cancellationToken);

                var generationsToAdd = new List<GenerationEntity>();

                foreach (var genDto in modelDto.Generations)
                {
                    if (genDto.YearFrom == null || genDto.YearFrom < 1900) 
                    {
                        continue; 
                    }

                    if (existingGenerations.Contains(genDto.Name)) continue;

                    var genResult = GenerationEntity.Create(
                        genDto.Name, 
                        modelEntity.Id, 
                        genDto.YearFrom.Value, 
                        genDto.YearTo
                    );

                    if (genResult.IsSuccess)
                    {
                        generationsToAdd.Add(genResult.Data);
                    }
                }

                if (generationsToAdd.Any())
                {
                    _dbContext.Generations.AddRange(generationsToAdd);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }

    private async Task<CategoryEntity> GetOrCreateCategoryAsync(string name, CancellationToken ct)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name, ct);
        if (category == null)
        {
            category = CategoryEntity.Create(name).Data;
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync(ct);
        }
        return category;
    }
}