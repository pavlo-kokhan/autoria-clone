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
using AutoriaClone.Domain.Results;
using AutoriaClone.Domain.Results.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AutoriaClone.Api.Application.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly JwtTokenOptions _jwtTokenOptions;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSenderService _emailSenderService;

    public IdentityService(
        UserManager<UserEntity> userManager,
        IOptions<JwtTokenOptions> options,
        IUnitOfWork unitOfWork,
        IEmailSenderService emailSenderService)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _emailSenderService = emailSenderService;
        _jwtTokenOptions = options.Value;
    }

    public async Task<Result<AccessTokenResponseDto>> RegisterUserAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        if (await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken) is not null)
            return UserValidationError.AlreadyExists;
        
        var user = new UserEntity { Email = email, UserName = email};
        var createUserResult = await _userManager.CreateAsync(user, password);

        if (!createUserResult.Succeeded)
            return AuthValidationError.RegistrationFailed;
        
        var roleAssignmentResult = await _userManager.AddToRoleAsync(user, nameof(Role.User));

        if (!roleAssignmentResult.Succeeded)
            return AuthValidationError.RoleAssignmentFailed;

        await SendConfirmationEmailAsync(user, cancellationToken);

        var refreshToken = new RefreshTokenValueObject(
            GenerateRefreshToken(),
            DateTime.UtcNow.AddSeconds(_jwtTokenOptions.RefreshTokenExpiresIn));
        
        return await GetAccessTokenAsync(user, refreshToken);
    }

    public async Task<Result> SendConfirmationEmailAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return AuthValidationError.InvalidUserNameOrPassword;

        if (user.EmailConfirmed)
            return Result.Success();
        
        await SendConfirmationEmailAsync(user, cancellationToken);
        
        return Result.Success();
    }

    public async Task<Result> ConfirmUserEmailAsync(int userId, string token, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return AuthValidationError.InvalidUserNameOrPassword;

        if (user.EmailConfirmed)
            return Result.Success();
        
        var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
        
        if (!confirmResult.Succeeded)
            return AuthValidationError.EmailConfirmationFailed;
        
        return Result.Success();
    }

    public async Task<Result<AccessTokenResponseDto>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
            return AuthValidationError.InvalidUserNameOrPassword;
        
        if (!await _userManager.CheckPasswordAsync(user, password))
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

    public async Task<Result> ChangePasswordAsync(int userId, string password, string newPassword, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return AuthValidationError.InvalidUserNameOrPassword;
        
        if (!user.EmailConfirmed)
            return AuthValidationError.EmailNotConfirmed;
        
        var changePasswordResult = await _userManager.ChangePasswordAsync(user, password, newPassword);
        
        if (!changePasswordResult.Succeeded)
            return AuthValidationError.ChangePasswordFailed;
        
        return Result.Success();
    }
    
    private async Task SendConfirmationEmailAsync(UserEntity user, CancellationToken cancellationToken)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        
        await _emailSenderService.SendEmailConfirmationAsync(user.Email!, token, user.Id, cancellationToken);
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
