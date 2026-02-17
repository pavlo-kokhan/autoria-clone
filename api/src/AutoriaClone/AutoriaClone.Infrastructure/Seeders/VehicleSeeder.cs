using System.Text.Json;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement;
using AutoriaClone.Infrastructure.Persistence;
using AutoriaClone.Infrastructure.Seeders.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoriaClone.Infrastructure.Seeders;

public class VehicleSeeder
{
    private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data", "car-brands.json");
    
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<VehicleSeeder> _logger;

    public VehicleSeeder(ApplicationDbContext dbContext, ILogger<VehicleSeeder> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Categories.AnyAsync(cancellationToken))
            return;

        if (!File.Exists(FilePath))
        {
            _logger.LogError("Failed to open seed data file. Vehicles data will not be seeded.");

            return;
        }

        List<BrandDto>? brandsData;
        
        try
        {
            var jsonContent = await File.ReadAllTextAsync(FilePath, cancellationToken);
        
            brandsData = JsonSerializer.Deserialize<List<BrandDto>>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (brandsData == null || !brandsData.Any())
            {
                _logger.LogError("Failed to read seed data file.");
            
                return;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to read seed data file with exception: {ex}");
            
            return;
        }

        var category = await SeedCategoriesAsync(cancellationToken);

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

    private async Task<CategoryEntity> SeedCategoriesAsync(CancellationToken cancellationToken)
    {
        var category = CategoryEntity.Create("Легкові").Data;
            
        await _dbContext.Categories.AddAsync(category, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return category;
    }
}