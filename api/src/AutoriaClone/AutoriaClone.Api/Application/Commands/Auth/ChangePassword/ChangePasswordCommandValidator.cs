using AutoriaClone.Domain.Aggregates.Validation;
using FluentValidation;

namespace AutoriaClone.Api.Application.Commands.Auth.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Password).Password();
        RuleFor(x => x.NewPassword).Password();
    }
}