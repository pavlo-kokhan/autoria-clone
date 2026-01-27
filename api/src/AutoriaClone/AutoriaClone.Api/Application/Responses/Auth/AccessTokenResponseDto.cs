namespace AutoriaClone.Api.Application.Responses.Auth;

public record AccessTokenResponseDto(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiration,
    DateTime RefreshTokenExpiration);
