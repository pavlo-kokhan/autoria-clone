using System.Text.RegularExpressions;
using FluentValidation;

namespace AutoriaClone.Api.Application.Extensions.Validation;

public static partial class UserValidationExtensions
{
    public static void Email<T>(this IRuleBuilderInitial<T, string> builder)
        => builder.NotEmpty().EmailAddress();

    public static void Password<T>(this IRuleBuilderInitial<T, string> builder)
        => builder.NotEmpty().Matches(PasswordRegex());
    
    [GeneratedRegex("^.{8,}$")]
    private static partial Regex PasswordRegex();
}