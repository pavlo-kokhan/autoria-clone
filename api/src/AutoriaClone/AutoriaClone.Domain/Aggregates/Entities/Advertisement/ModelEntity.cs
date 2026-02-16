using AutoriaClone.Domain.Aggregates.Abstract;
using AutoriaClone.Domain.Aggregates.Validation;
using AutoriaClone.Domain.Results.Generic;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement;

public class ModelEntity : PersistenceEntity
{
    private static readonly ModelEntityValidator Validator = new();
    
    private ModelEntity(string name, int makeId)
    {
        Name = name;
        MakeId = makeId;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ModelEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    { }

    public string Name { get; private set; }
    
    public int MakeId { get; private set; }

    public ICollection<GenerationEntity> Generations { get; private set; } = new List<GenerationEntity>();

    public static Result<ModelEntity> Create(string name, int makeId)
    {
        var entity = new ModelEntity(name, makeId);

        return Validator.ToResult(entity);
    }
}