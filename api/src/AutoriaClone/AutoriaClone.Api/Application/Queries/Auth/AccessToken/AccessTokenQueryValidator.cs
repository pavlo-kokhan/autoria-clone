using System.Text.RegularExpressions;
using AutoriaClone.Api.Application.Extensions.Validation;
using FluentValidation;

namespace AutoriaClone.Api.Application.Queries.Auth.AccessToken;

public partial class AccessTokenQueryValidator : AbstractValidator<AccessTokenQuery>
{
    public AccessTokenQueryValidator()
    {
        RuleFor(q => q.Email).Email();
        RuleFor(q => q.Password).Password();
    }
}
