using AutoriaClone.Domain.ValidationErrors;

namespace AutoriaClone.Api.Application.Constants.ValidationErrors;

public static class FileValidationError
{
    public static readonly ValidationError FailedToUpload = ValidationError.CreateInvalidArgument("FILE_FAILED_TO_UPLOAD");
}