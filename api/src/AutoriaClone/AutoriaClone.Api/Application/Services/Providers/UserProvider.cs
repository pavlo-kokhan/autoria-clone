using System.Security.Authentication;
using System.Security.Claims;
using AutoriaClone.Domain.Providers.Abstract;

namespace AutoriaClone.Api.Application.Services.Providers;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserProvider(IHttpContextAccessor httpContextAccessor) 
        => _httpContextAccessor = httpContextAccessor;

    public int Id 
        => int.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                     ?? throw new AuthenticationException("UserId is not provided in claims"));
}