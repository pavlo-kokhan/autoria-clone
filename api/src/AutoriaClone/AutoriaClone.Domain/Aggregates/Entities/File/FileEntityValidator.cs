using System.Text.RegularExpressions;
using FluentValidation;

namespace AutoriaClone.Domain.Aggregates.Entities.File;

public partial class FileEntityValidator : AbstractValidator<FileEntity>
{
    public FileEntityValidator()
    {
        RuleFor(f => f.Key).NotEmpty();
        RuleFor(f => f.Extension).Matches(GetFileExtensionRegex());
    }

    [GeneratedRegex("\\.(\\w+)$")]
    private static partial Regex GetFileExtensionRegex();
}
