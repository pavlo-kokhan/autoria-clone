namespace AutoriaClone.Api.Application.Services.Abstract;

public interface IBlobsConnectionVerifier
{
    Task CheckConnectionAsync();
    
    Task EnsureContainersExistsAsync();
}