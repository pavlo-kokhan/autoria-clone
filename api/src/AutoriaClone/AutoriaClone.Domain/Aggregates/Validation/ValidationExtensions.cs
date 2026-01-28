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
        => builder.NotEmpty().EmailAddress();

    public static void Password<T>(this IRuleBuilderInitial<T, string> builder)
        => builder.NotEmpty().Matches(PasswordRegex());
    
    public static void FirstNameOptional<T>(this IRuleBuilderInitial<T, string?> builder)
        => builder
            .MaximumLength(50)
            .When(x => x is not null);
    
    public static void LastNameOptional<T>(this IRuleBuilderInitial<T, string?> builder)
        => builder
            .MaximumLength(50)
            .When(x => x is not null);
    
    public static void PhoneNumberOptional<T>(this IRuleBuilderInitial<T, string?> builder)
        => builder
            .Must(x => x is not null && x.StartsWith("+"))
            .MaximumLength(15)
            .When(x => x is not null);
    
    public static void TelegramUserNameOptional<T>(this IRuleBuilderInitial<T, string?> builder)
        => builder
            .Must(x => x is not null && x.StartsWith("@"))
            .MaximumLength(32)
            .When(x => x is not null);
    
    [GeneratedRegex("^.{8,}$")]
    private static partial Regex PasswordRegex();
}