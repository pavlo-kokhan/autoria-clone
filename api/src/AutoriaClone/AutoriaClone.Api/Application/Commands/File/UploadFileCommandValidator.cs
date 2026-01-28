using FluentValidation;

namespace AutoriaClone.Api.Application.Commands.File;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNull()
            .Must(f => f.Length <= 3_000_000);
    }
}