using AutoriaClone.Domain.Aggregates.Validation;
using FluentValidation;

namespace AutoriaClone.Api.Application.Commands.File;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(x => x.FileName).FileName();
        RuleFor(x => x.ContentType).FileContentType();
        RuleFor(x => x.FileSize).FileSize();
    }
}