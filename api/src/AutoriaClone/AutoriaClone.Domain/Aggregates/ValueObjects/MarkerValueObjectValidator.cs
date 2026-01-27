using AutoriaClone.Domain.Aggregates.Attributes;
using FluentValidation;

namespace AutoriaClone.Domain.Aggregates.ValueObjects;

[ValueObjectValidator]
public class MarkerValueObjectValidator : AbstractValidator<MarkerValueObject>
{
    public MarkerValueObjectValidator()
    {
        RuleFor(x => x.Marker).NotNull();
    }
}