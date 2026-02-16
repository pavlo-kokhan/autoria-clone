namespace AutoriaClone.Domain.Aggregates.Abstract;

public interface ITimeRelatedEntity
{
    DateTime CreatedAt { get; }
    
    DateTime UpdatedAt { get; }
}