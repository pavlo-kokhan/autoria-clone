namespace AutoriaClone.Domain.Aggregates.Abstract;

public interface IPersistenceEntity
{
    bool IsDeleted { get; }

    DateTime? DeletedTime { get; }
}
