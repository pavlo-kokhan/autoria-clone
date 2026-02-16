using AutoriaClone.Domain.Aggregates.Abstract;
using AutoriaClone.Domain.Aggregates.Validation;
using AutoriaClone.Domain.Results.Generic;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement;

public class MakeEntity : PersistenceEntity
{
    private static readonly MakeEntityValidator Validator = new();
    
    private MakeEntity(string name, int categoryId)
    {
        Name = name;
        CategoryId = categoryId;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private MakeEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    { }
    
    public string Name { get; private set; }
    
    // todo: this part is not from real world
    // as it is assumed to have only 1 category of vehicles - id = 1 name = Легкові
    public int CategoryId { get; private set; }

    public ICollection<ModelEntity> Models { get; private set; } = new List<ModelEntity>();

    public static Result<MakeEntity> Create(string name, int categoryId)
    {
        var entity = new MakeEntity(name, categoryId);

        return Validator.ToResult(entity);
    }
}