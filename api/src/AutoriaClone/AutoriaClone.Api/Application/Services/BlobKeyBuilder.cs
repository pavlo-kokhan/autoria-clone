namespace AutoriaClone.Api.Application.Services;

public class BlobKeyBuilder
{
    private string _folder = string.Empty;
    private string _ownerId = string.Empty;
    private string _fileName = string.Empty;
    private string _fileExtension = string.Empty;

    public BlobKeyBuilder() 
    { }

    public static BlobKeyBuilder Create() => new();
    
    public BlobKeyBuilder InFolder(string folder)
    {
        if (folder.Contains('/'))
            throw new InvalidOperationException(nameof(folder));

        _folder = folder;
        
        return this;
    }

    public BlobKeyBuilder WithOwner(int ownerId)
    {
        _ownerId = ownerId.ToString();        
        
        return this;
    }

    public BlobKeyBuilder WithName(string fileName, string? extension = null)
    {
        if (fileName.Contains('/'))
            throw new InvalidOperationException(nameof(fileName));
        
        _fileName = fileName;
        _fileExtension = extension ?? string.Empty;
        
        return this;
    }

    public string Build() 
        => $"{_folder}/{_ownerId}/{_fileName}-{Guid.NewGuid():N}{_fileExtension}";

    public override string ToString()
        => Build();
}