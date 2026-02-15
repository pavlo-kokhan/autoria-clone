using AutoriaClone.Domain.ValidationErrors;

namespace AutoriaClone.Api.Application.Constants.ValidationErrors;

public static class AuthValidationError
{
    public static readonly ValidationError InvalidRefreshToken = ValidationError.CreateInvalidArgument("INVALID_REFRESH_TOKEN");
    
    public static readonly ValidationError InvalidUserNameOrPassword = ValidationError.CreateInvalidArgument("INVALID_USER_NAME_OR_PASSWORD");
    
    public static readonly ValidationError RegistrationFailed = ValidationError.CreateInvalidArgument("REGISTRATION_FAILED");
    
    public static readonly ValidationError RoleAssignmentFailed = ValidationError.CreateInvalidArgument("ROLE_ASSIGNMENT_FAILED");
    
    public static readonly ValidationError EmailNotConfirmed = ValidationError.CreateInvalidArgument("EMAIL_NOT_CONFIRMED");
    
    public static readonly ValidationError EmailConfirmationFailed = ValidationError.CreateInvalidArgument("EMAIL_CONFIRMATION_FAILED");
    
    public static readonly ValidationError ChangePasswordFailed = ValidationError.CreateInvalidArgument("CHANGE_PASSWORD_FAILED");
}
