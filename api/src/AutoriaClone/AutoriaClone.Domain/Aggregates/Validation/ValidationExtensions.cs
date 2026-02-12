using System.Linq.Expressions;
using System.Text.RegularExpressions;
using AutoriaClone.Domain.Results;
using AutoriaClone.Domain.Results.Generic;
using AutoriaClone.Domain.ValidationErrors;
using FluentValidation;

namespace AutoriaClone.Domain.Aggregates.Validation;

public static partial class ValidationExtensions
{
    public static Result<TEntity> ToResult<TEntity>(this IValidator<TEntity> entityValidator, TEntity entity, params Expression<Func<TEntity, object?>>[] propertiesSelector)
    {
        var validationResult = propertiesSelector.Length > 0
            ? entityValidator.Validate(entity, strategy => strategy.IncludeProperties(propertiesSelector))
            : entityValidator.Validate(entity);

        if (!validationResult.IsValid)
            return Result<TEntity>.Failure(
                validationResult
                    .Errors
                    .DistinctBy(e => e.ErrorCode)
                    .ToDictionary(e => e.ErrorCode, e => ValidationError.CreatePropertyValidation(e.ErrorCode, e.ErrorMessage, e.PropertyName)),
                null,
                ResultStatus.InvalidArgument);

        return entity;
    }
    
    public static void Email<T>(this IRuleBuilderInitial<T, string> builder)
        => builder
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email address.");

    public static void Password<T>(this IRuleBuilderInitial<T, string> builder)
        => builder
            .NotEmpty()
            .Matches(PasswordRegex())
            .WithMessage("Invalid password structure or size.");
    
    public static void FirstNameOptional<T>(this IRuleBuilderInitial<T, string?> builder)
        => builder
            .MaximumLength(50)
            .When(x => x is not null)
            .WithMessage("First name should be less than 50 characters.");
    
    public static void LastNameOptional<T>(this IRuleBuilderInitial<T, string?> builder)
        => builder
            .MaximumLength(50)
            .When(x => x is not null)
            .WithMessage("Last name should be less than 50 characters.");
    
    public static void PhoneNumberOptional<T>(this IRuleBuilderInitial<T, string?> builder)
        => builder
            .Must(x => x is not null && x.StartsWith("+"))
            .MaximumLength(15)
            .When(x => x is not null)
            .WithMessage("Invalid phone number. Phone number should start with + character and be less then 15 characters.");
    
    public static void TelegramUserNameOptional<T>(this IRuleBuilderInitial<T, string?> builder)
        => builder
            .Must(x => x is not null && x.StartsWith("@"))
            .MaximumLength(32)
            .When(x => x is not null)
            .WithMessage("Invalid telegram user name.");
    
    public static void FileName<T>(this IRuleBuilderInitial<T, string> builder)
    {
        builder
            .NotEmpty()
            .Must(name =>
            {
                var ext = Path.GetExtension(name).ToLowerInvariant();
                
                return MediaValidationConstants.AllowedExtensions.Contains(ext);
            })
            .WithMessage($"Unsupported file extension. Allowed: {string.Join(' ', MediaValidationConstants.AllowedExtensions)}");
    }

    public static void FileContentType<T>(this IRuleBuilderInitial<T, string> builder)
    {
        builder
            .NotEmpty()
            .Must(type =>
            {
                var t = type.ToLowerInvariant();
                
                return MediaValidationConstants.AllowedMediaTypes.Contains(t);
            })
            .WithMessage($"Unsupported content type. Allowed: {string.Join(' ', MediaValidationConstants.AllowedMediaTypes)}");
    }

    public static void FileSize<T>(this IRuleBuilder<T, long> builder)
    {
        builder
            .GreaterThan(0)
            .WithMessage("File size must be greater than 0.")
            .LessThan(MediaValidationConstants.MaxFileSize)
            .WithMessage($"File size must be less than {MediaValidationConstants.MaxFileSize}");
    }
    
    [GeneratedRegex("^.{8,}$")]
    private static partial Regex PasswordRegex();
}