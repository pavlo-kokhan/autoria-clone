using AutoriaClone.Domain.Aggregates.Attributes;
using FluentValidation;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.ValueObjects;

[ValueObjectValidator]
public class PowerValueObjectValidator : AbstractValidator<PowerValueObject>
{
    public PowerValueObjectValidator()
    {
        // todo: add rules
    }
}