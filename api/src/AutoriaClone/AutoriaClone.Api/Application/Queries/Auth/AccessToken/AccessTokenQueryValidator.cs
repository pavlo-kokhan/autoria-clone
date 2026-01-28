using AutoriaClone.Domain.Aggregates.Validation;
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
