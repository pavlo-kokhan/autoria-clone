using AutoriaClone.Domain.ValidationErrors;

namespace AutoriaClone.Api.Application.Constants.ValidationErrors;

public class EmailValidationError
{
    public static readonly ValidationError FailedToSend = ValidationError.CreateInvalidArgument("EMAIL_FAILED_TO_SEND");
}