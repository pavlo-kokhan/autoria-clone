using System.Linq.Expressions;
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
}