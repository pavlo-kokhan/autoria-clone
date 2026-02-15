using AutoriaClone.Domain.Aggregates.Validation;
using AutoriaClone.Domain.Aggregates.ValueObjects.Address;
using AutoriaClone.Domain.Results;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AutoriaClone.Domain.Aggregates.Entities.User;

public class UserEntity : IdentityUser<int>
{
    private const int MaxActiveRefreshTokens = 5;
    private readonly List<RefreshTokenValueObject> _refreshTokens = [];
    private static readonly IValidator<UserEntity> Validator = new UserEntityValidator(new AddressValueObjectValidator());
    
    public IReadOnlyCollection<RefreshTokenValueObject> RefreshTokens => _refreshTokens;

    public string? FirstName { get; private set; }
    
    public string? LastName { get; private set; }
    
    public string? TelegramUserName { get; set; }
    
    public string? WebSiteUrl { get; set; }
    
    public AddressValueObject? Address { get; private set; }
    
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
    
    public Result Update(
        string? firstName,
        string? lastName,
        string? phoneNumber,
        string? telegramUserName,
        string? webSiteUrl,
        AddressValueObject? address)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        TelegramUserName = telegramUserName;
        WebSiteUrl = webSiteUrl;
        Address = address;

        return Validator.ToResult(this);
    }
}