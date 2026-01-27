using AutoriaClone.Domain.Results;
using AutoriaClone.Domain.ValidationErrors;

namespace AutoriaClone.Api.Application.Constants.ValidationErrors;

public static class UserValidationError
{
    public static readonly ValidationError NotFound = ValidationError.CreateInvalidArgument("USER_NOT_FOUND", ResultStatus.NotFound);
    
    public static readonly ValidationError AlreadyExists = ValidationError.CreateInvalidArgument("USER_ALREADY_EXISTS");
}
