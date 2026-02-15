using AutoriaClone.Domain.Aggregates.Validation;
using FluentValidation;

namespace AutoriaClone.Api.Application.Queries.User;

public class UserQueryValidator : AbstractValidator<UserQuery>
{
    public UserQueryValidator()
    {
        RuleFor(x => x.Id).IdOptional();
    }
}