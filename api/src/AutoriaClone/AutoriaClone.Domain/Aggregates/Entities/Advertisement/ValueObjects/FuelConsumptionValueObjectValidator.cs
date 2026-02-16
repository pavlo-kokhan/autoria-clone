using AutoriaClone.Domain.Aggregates.Attributes;
using FluentValidation;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.ValueObjects;

[ValueObjectValidator]
public class FuelConsumptionValueObjectValidator : AbstractValidator<FuelConsumptionValueObject>
{
    public FuelConsumptionValueObjectValidator()
    {
        // todo: add rules
    }
}