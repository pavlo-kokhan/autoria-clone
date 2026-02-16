using AutoriaClone.Domain.Aggregates.Attributes;
using FluentValidation;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.ValueObjects;

[ValueObjectValidator]
public class VehicleDetailsValueObjectValidator : AbstractValidator<VehicleDetailsValueObject>
{
    public VehicleDetailsValueObjectValidator()
    {
        // todo: add rules
    }
}