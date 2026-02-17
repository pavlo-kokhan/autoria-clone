using AutoriaClone.Domain.ValidationErrors;

namespace AutoriaClone.Api.Application.Constants.ValidationErrors;

public static class VehicleValidationErrors
{
    public static readonly ValidationError CategoryNotFound = ValidationError.CreateInvalidArgument("CATEGORY_NOT_FOUND");
    
    public static readonly ValidationError MakeNotFound = ValidationError.CreateInvalidArgument("MAKE_NOT_FOUND");
}