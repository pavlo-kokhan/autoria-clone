using FluentValidation;

namespace AutoriaClone.Api.Application.Commands.Auth.ConfirmEmail;

public class ConfirmUserEmailCommandValidator : AbstractValidator<ConfirmUserEmailCommand>
{
    public ConfirmUserEmailCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("User confirmation token is required.");
    }
}