using AutoriaClone.Domain.Aggregates.Abstract;
using AutoriaClone.Domain.Aggregates.Validation;
using AutoriaClone.Domain.Results.Generic;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement;

public class CategoryEntity : PersistenceEntity
{
    private static readonly CategoryEntityValidator Validator = new();
    
    private CategoryEntity(string name)
    {
        Name = name;
    }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private CategoryEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    { }
    
    public string Name { get; private set; }

    public ICollection<MakeEntity> Makes { get; private set; } = new List<MakeEntity>();

    public static Result<CategoryEntity> Create(string name)
    {
        var entity = new CategoryEntity(name);

        return Validator.ToResult(entity);
    }
}