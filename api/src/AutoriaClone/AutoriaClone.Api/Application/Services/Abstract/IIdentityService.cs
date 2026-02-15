using AutoriaClone.Api.Application.Responses.Auth;
using AutoriaClone.Api.Application.Services.Identity;
using AutoriaClone.Domain.Results;
using AutoriaClone.Domain.Results.Generic;

namespace AutoriaClone.Api.Application.Services.Abstract;

public interface IIdentityService
{
    Task<Result<AccessTokenResponseDto>> RegisterUserAsync(string email, string password, CancellationToken cancellation = default);

    Task<Result> SendConfirmationEmailAsync(int userId, CancellationToken cancellationToken);

    Task<Result> ConfirmUserEmailAsync(int userId, string token, CancellationToken cancellationToken = default);
    
    Task<Result<AccessTokenResponseDto>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken = default);

    Task<Result<AccessTokenResponseDto>> GetAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<Result> ChangePasswordAsync(int userId, string password, string newPassword, CancellationToken cancellationToken = default);
}
