using AutoriaClone.Domain.ValidationErrors;

namespace AutoriaClone.Api.Application.Constants.ValidationErrors;

public static class AuthValidationError
{
    public static readonly ValidationError InvalidRefreshToken = ValidationError.CreateInvalidArgument("INVALID_REFRESH_TOKEN");
    public static readonly ValidationError InvalidUserNameOrPassword = ValidationError.CreateInvalidArgument("INVALID_USER_NAME_OR_PASSWORD");
}
