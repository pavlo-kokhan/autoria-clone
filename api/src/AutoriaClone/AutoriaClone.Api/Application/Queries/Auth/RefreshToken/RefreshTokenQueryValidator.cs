using FluentValidation;

namespace AutoriaClone.Api.Application.Queries.Auth.RefreshToken;

public class RefreshTokenQueryValidator : AbstractValidator<RefreshTokenQuery>
{
    public RefreshTokenQueryValidator()
        => RuleFor(q => q.RefreshToken).NotEmpty();
}
