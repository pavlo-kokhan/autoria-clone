using AutoriaClone.Domain.Aggregates.Abstract;
using AutoriaClone.Domain.Aggregates.Validation;
using AutoriaClone.Domain.Results.Generic;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement;

public class GenerationEntity : PersistenceEntity
{
    private static readonly GenerationEntityValidator Validator = new();
    
    private GenerationEntity(string name, int modelId, int yearFrom, int? yearTo)
    {
        Name = name;
        ModelId = modelId;
        YearFrom = yearFrom;
        YearTo = yearTo;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private GenerationEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    { }

    public string Name { get; private set; }
    
    public int ModelId { get; private set; }
    
    public int YearFrom { get; private set; }
    
    public int? YearTo { get; private set; }

    public static Result<GenerationEntity> Create(string name, int modelId, int yearFrom, int? yearTo)
    {
        var entity = new GenerationEntity(name, modelId, yearFrom, yearTo);

        return Validator.ToResult(entity);
    }
}