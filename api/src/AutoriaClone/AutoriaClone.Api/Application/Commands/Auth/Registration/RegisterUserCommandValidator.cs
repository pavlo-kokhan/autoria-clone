using AutoriaClone.Domain.Aggregates.Validation;
using FluentValidation;

namespace AutoriaClone.Api.Application.Commands.Auth.Registration;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(q => q.Email).Email();
        RuleFor(q => q.Password).Password();
    }
}