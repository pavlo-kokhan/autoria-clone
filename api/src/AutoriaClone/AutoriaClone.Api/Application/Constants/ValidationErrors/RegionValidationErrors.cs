using AutoriaClone.Domain.ValidationErrors;

namespace AutoriaClone.Api.Application.Constants.ValidationErrors;

public class RegionValidationErrors
{
    public static readonly ValidationError RegionsNotFound = ValidationError.CreateInvalidArgument("REGIONS_NOT_FOUND");
    
    public static readonly ValidationError CitiesNotFound = ValidationError.CreateInvalidArgument("CITIES_NOT_FOUND");
}