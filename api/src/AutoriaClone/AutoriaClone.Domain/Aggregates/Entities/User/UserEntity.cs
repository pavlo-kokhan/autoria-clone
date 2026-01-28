using AutoriaClone.Domain.Aggregates.Validation;
using AutoriaClone.Domain.Results;
using Microsoft.AspNetCore.Identity;

namespace AutoriaClone.Domain.Aggregates.Entities.User;

public class UserEntity : IdentityUser<int>
{
    private const int MaxActiveRefreshTokens = 5;
    private readonly List<RefreshTokenValueObject> _refreshTokens = [];
    private static readonly UserContactsValueObjectValidator ContactsValidator = new();
    
    public IReadOnlyCollection<RefreshTokenValueObject> RefreshTokens => _refreshTokens;

    public UserContactsValueObject? Contacts { get; private set; }
    
    public void AddRefreshToken(RefreshTokenValueObject refreshToken)
    {
        _refreshTokens.RemoveAll(rt => rt.IsExpired);

        if (_refreshTokens.Count >= MaxActiveRefreshTokens)
        {
            var toRemove = _refreshTokens
                .Where(t => !t.IsExpired)
                .OrderBy(t => t.ExpiresAt)
                .FirstOrDefault();

            if (toRemove is not null)
                _refreshTokens.Remove(toRemove);
        }

        _refreshTokens.Add(refreshToken);
    }

    public RefreshTokenValueObject? GetRefreshToken(string refreshToken)
        => _refreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);
    
    public Result UpdateContacts(UserContactsValueObject contacts)
    {
        Contacts = contacts;

        return ContactsValidator.ToResult(Contacts);
    }
}