using AutoriaClone.Domain.Aggregates.ValueObjects.Address;

namespace AutoriaClone.Api.Application.Responses.User;

public record UserResponseDto(
    string Email,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? TelegramUserName,
    string? WebSiteUrl,
    AddressValueObject? Address);