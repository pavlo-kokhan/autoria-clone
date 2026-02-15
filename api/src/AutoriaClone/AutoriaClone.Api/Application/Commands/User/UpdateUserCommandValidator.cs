using AutoriaClone.Domain.Aggregates.Validation;
using AutoriaClone.Domain.Aggregates.ValueObjects.Address;
using FluentValidation;

namespace AutoriaClone.Api.Application.Commands.User;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IValidator<AddressValueObject> addressValueObjectValidator)
    {
        RuleFor(x => x.FirstName).FirstNameOptional(x => x.FirstName);
        RuleFor(x => x.LastName).LastNameOptional(x => x.LastName);
        RuleFor(x => x.PhoneNumber).PhoneNumberOptional(x => x.PhoneNumber);
        RuleFor(x => x.TelegramUserName).TelegramUserNameOptional(x => x.TelegramUserName);
        RuleFor(x => x.WebSiteUrl).WebSiteUrlOptional(x => x.WebSiteUrl);
        RuleFor(x => x.Address).SetValidator(addressValueObjectValidator!);
    }
}