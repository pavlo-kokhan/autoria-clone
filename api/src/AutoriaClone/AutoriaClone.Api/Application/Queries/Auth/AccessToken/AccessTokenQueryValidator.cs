using System.Text.RegularExpressions;
using FluentValidation;

namespace AutoriaClone.Api.Application.Queries.Auth.AccessToken;

public partial class AccessTokenQueryValidator : AbstractValidator<AccessTokenQuery>
{
    public AccessTokenQueryValidator()
    {
        RuleFor(q => q.UserName).NotEmpty();
        RuleFor(q => q.Password).Matches(PasswordRegex());
    }

    [GeneratedRegex("^.{8,}$")]
    private static partial Regex PasswordRegex();
}
