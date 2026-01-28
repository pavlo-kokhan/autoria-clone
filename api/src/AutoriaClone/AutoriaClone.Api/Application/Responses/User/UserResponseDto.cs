namespace AutoriaClone.Api.Application.Responses.User;

public record UserResponseDto(
    string Email,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? TelegramUserName);