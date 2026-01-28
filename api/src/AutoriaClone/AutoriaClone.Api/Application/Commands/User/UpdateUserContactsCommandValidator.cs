using AutoriaClone.Domain.Aggregates.Validation;
using FluentValidation;

namespace AutoriaClone.Api.Application.Commands.User;

public class UpdateUserContactsCommandValidator : AbstractValidator<UpdateUserContactsCommand>
{
    public UpdateUserContactsCommandValidator()
    {
        RuleFor(x => x.FirstName).FirstNameOptional();
        RuleFor(x => x.LastName).LastNameOptional();
        RuleFor(x => x.PhoneNumber).PhoneNumberOptional();
        RuleFor(x => x.TelegramUserName).TelegramUserNameOptional();
    }
}