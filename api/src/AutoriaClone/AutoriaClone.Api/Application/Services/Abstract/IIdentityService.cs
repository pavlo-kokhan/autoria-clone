using AutoriaClone.Api.Application.Responses.Auth;
using AutoriaClone.Domain.Results.Generic;

namespace AutoriaClone.Api.Application.Services.Abstract;

public interface IIdentityService
{
    Task<Result<AccessTokenResponseDto>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken = default);

    Task<Result<AccessTokenResponseDto>> GetAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}
