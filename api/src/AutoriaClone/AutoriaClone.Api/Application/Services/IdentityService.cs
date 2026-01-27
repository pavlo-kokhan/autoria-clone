using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Options;
using AutoriaClone.Api.Application.Responses.Auth;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain;
using AutoriaClone.Domain.Aggregates.Entities.User;
using AutoriaClone.Domain.Constants;
using AutoriaClone.Domain.Results.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AutoriaClone.Api.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly JwtTokenOptions _jwtTokenOptions;
    private readonly IUnitOfWork _unitOfWork;

    public IdentityService(UserManager<UserEntity> userManager, IOptions<JwtTokenOptions> options, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _jwtTokenOptions = options.Value;
    }

    public async Task<Result<AccessTokenResponseDto>> RegisterUserAsync(string email, string password, CancellationToken cancellation = default)
    {
        if (await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellation) is not null)
            return UserValidationError.AlreadyExists;
        
        var user = new UserEntity { Email = email, UserName = email};
        var createUserResult = await _userManager.CreateAsync(user, password);

        if (!createUserResult.Succeeded)
            return AuthValidationError.RegistrationFailed;
        
        var roleAssignmentResult = await _userManager.AddToRoleAsync(user, nameof(Role.User));

        if (!roleAssignmentResult.Succeeded)
            return AuthValidationError.RoleAssignmentFailed;
        
        var refreshToken = new RefreshTokenValueObject(
            GenerateRefreshToken(),
            DateTime.UtcNow.AddSeconds(_jwtTokenOptions.RefreshTokenExpiresIn));
        
        return await GetAccessTokenAsync(user, refreshToken);
    }

    public async Task<Result<AccessTokenResponseDto>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
            return AuthValidationError.InvalidUserNameOrPassword;

        var isValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!isValidPassword)
            return AuthValidationError.InvalidUserNameOrPassword;

        var refreshToken = new RefreshTokenValueObject(
            GenerateRefreshToken(),
            DateTime.UtcNow.AddSeconds(_jwtTokenOptions.RefreshTokenExpiresIn));

        var accessToken = await GetAccessTokenAsync(user, refreshToken);

        user.AddRefreshToken(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return accessToken;
    }

    public async Task<Result<AccessTokenResponseDto>> GetAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);

        if (user is null)
            return UserValidationError.NotFound;

        var userRefreshToken = user.GetRefreshToken(refreshToken);

        if (userRefreshToken is null || userRefreshToken.ExpiresAt < DateTime.UtcNow)
            return AuthValidationError.InvalidRefreshToken;

        return await GetAccessTokenAsync(user, userRefreshToken);
    }

    private async Task<AccessTokenResponseDto> GetAccessTokenAsync(UserEntity user, RefreshTokenValueObject refreshToken)
    {
        var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        if (userRole is not null)
            claims.Add(new Claim(ClaimTypes.Role, userRole));

        var accessToken = GetAccessToken(claims);

        return new AccessTokenResponseDto(
            accessToken.Token,
            refreshToken.Token,
            accessToken.Expiry,
            refreshToken.ExpiresAt);
    }

    private static string GenerateRefreshToken()
        => string.Join(string.Empty, SHA256.HashData(Guid.NewGuid().ToByteArray()).Select(b => b.ToString("x2")));

    private (string Token, DateTime Expiry) GetAccessToken(IEnumerable<Claim> claims)
    {
        var claimsIdentity = new ClaimsIdentity(claims);
        var jwtSecurityToken = new JwtSecurityToken(
            claims: claimsIdentity.Claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(_jwtTokenOptions.ExpiresIn),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_jwtTokenOptions.Key), SecurityAlgorithms.HmacSha256));

        return(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), jwtSecurityToken.ValidTo);
    }
}
