using AutoriaClone.Domain.Aggregates.Attributes;
using FluentValidation;

namespace AutoriaClone.Domain.Aggregates.ValueObjects.Address;

[ValueObjectValidator]
public class AddressValueObjectValidator : AbstractValidator<AddressValueObject>
{
    public AddressValueObjectValidator()
    {
        // todo: add rules
    }
}