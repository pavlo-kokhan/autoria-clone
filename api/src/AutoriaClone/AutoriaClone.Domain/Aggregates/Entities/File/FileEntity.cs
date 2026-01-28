using AutoriaClone.Domain.Aggregates.Abstract;
using AutoriaClone.Domain.Aggregates.Validation;
using AutoriaClone.Domain.Results.Generic;

namespace AutoriaClone.Domain.Aggregates.Entities.File;

public class FileEntity : PersistenceEntity, IUserRelatedEntity
{
    private static readonly FileEntityValidator Validator = new();

    private FileEntity(string key, string extension, int userId)
    {
        Key = key;
        Extension = extension;
        UserId = userId;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private FileEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    { }

    public string Key { get; private set; }

    public string Extension { get; private set; }
    
    public int UserId { get; private set; }

    public static Result<FileEntity> Create(string key, string extension, int userId)
        => Validator.ToResult(new FileEntity(key, extension, userId));

}
