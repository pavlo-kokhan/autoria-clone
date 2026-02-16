using AutoriaClone.Domain.Aggregates.Attributes;
using FluentValidation;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.ValueObjects;

[ValueObjectValidator]
public class PriceValueObjectValidator : AbstractValidator<PriceValueObject>
{
    public PriceValueObjectValidator()
    {
        // todo: add rules
    }
}