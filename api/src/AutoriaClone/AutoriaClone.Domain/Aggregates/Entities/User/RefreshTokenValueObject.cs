namespace AutoriaClone.Domain.Aggregates.Entities.User;

public record RefreshTokenValueObject(string Token, DateTime ExpiresAt)
{
    public bool IsExpired => ExpiresAt <= DateTime.UtcNow;
}