using AutoriaClone.Api.Application.Extensions.Validation;
using FluentValidation;

namespace AutoriaClone.Api.Application.Queries.Auth.Registration;

public class RegistrationAccessTokenQueryValidator : AbstractValidator<RegistrationAccessTokenQuery>
{
    public RegistrationAccessTokenQueryValidator()
    {
        RuleFor(q => q.Email).Email();
        RuleFor(q => q.Password).Password();
    }
}