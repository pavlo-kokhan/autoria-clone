using AutoriaClone.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace AutoriaClone.Api.Filters;

public class AppAuthorizeAttribute : AuthorizeAttribute
{
    public AppAuthorizeAttribute(Role role)
    {
        var roleList = Enum
            .GetValues<Role>()
            .Where(r => role.HasFlag(r))
            .Select(r => r.ToString());

        Roles = string.Join(",", roleList);
    }
}
