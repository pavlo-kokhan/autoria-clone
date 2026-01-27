using AutoriaClone.Domain.Results;
using AutoriaClone.Domain.Results.Generic;
using AutoriaClone.Domain.ValidationErrors;
using FluentValidation;
using MediatR;

namespace AutoriaClone.Api.Pipelines;

public class ValidationPipelineGeneric<TRequest, TData> : IPipelineBehavior<TRequest, Result<TData>>
    where TRequest : IRequest<Result<TData>>
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationPipelineGeneric(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public Task<Result<TData>> Handle(TRequest request, RequestHandlerDelegate<Result<TData>> next, CancellationToken cancellationToken)
    {
        var validator = _serviceProvider.GetService<IValidator<TRequest>>();

        if (validator is null)
            return next(cancellationToken);

        var validationResult = validator.Validate(request);

        return !validationResult.IsValid
            ? Task.FromResult(
                Result<TData>.Failure(
                    validationResult
                        .Errors
                        .DistinctBy(e => e.ErrorCode)
                        .ToDictionary(e => e.ErrorCode, e => ValidationError.CreatePropertyValidation(e.ErrorCode, e.ErrorMessage, e.PropertyName)),
                    null,
                    ResultStatus.InvalidArgument))
            : next(cancellationToken);
    }
}
