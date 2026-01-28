using AutoriaClone.Domain.Aggregates.Attributes;
using AutoriaClone.Domain.Aggregates.Validation;
using FluentValidation;

namespace AutoriaClone.Domain.Aggregates.Entities.User;

[ValueObjectValidator]
public class UserContactsValueObjectValidator : AbstractValidator<UserContactsValueObject>
{
    public UserContactsValueObjectValidator()
    {
        RuleFor(x => x.FirstName).FirstNameOptional();
        RuleFor(x => x.LastName).LastNameOptional();
        RuleFor(x => x.PhoneNumber).PhoneNumberOptional();
        RuleFor(x => x.TelegramUserName).TelegramUserNameOptional();
    }
}