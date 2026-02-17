using AutoriaClone.Api.Application.Responses.Vehicle.Lookup;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement.Flags;

namespace AutoriaClone.Api.Application.Helpers;

public static class LookupHelper
{
    public static LookupsResponseDto GetLookups()
    {
        var fuelTypes = Enum.GetValues<FuelType>()
            .Where(x => x != 0)
            .Select(x => new LookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString()))
            .ToList();

        var transmissions = Enum.GetValues<TransmissionType>()
            .Where(x => x != 0)
            .Select(x => new LookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString()))
            .ToList();
        
        var euroStandards = Enum.GetValues<EuroStandardType>()
            .Where(x => x != 0)
            .Select(x => new LookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString()))
            .ToList();
        
        var driveTypes = Enum.GetValues<VehicleDriveType>()
            .Where(x => x != 0)
            .Select(x => new LookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString()))
            .ToList();

        var colorShades = Enum.GetValues<ColorShadeType>()
            .Where(x => x != 0)
            .Select(x => new ColorLookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString(), 
                x.ToHexCode()))
            .ToList();

        var paintworkConditions = Enum.GetValues<PaintworkConditionType>()
            .Where(x => x != 0)
            .Select(x => new DescriptionLookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString(), 
                x.ToDescription()))
            .ToList();

        var conditions = Enum.GetValues<ConditionType>()
            .Where(x => x != 0)
            .Select(x => new DescriptionLookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString(), 
                x.ToDescription()))
            .ToList();
        
        var comfortOptions = Enum.GetValues<ComfortOptions>()
            .Where(x => x != 0)
            .Select(x => new LookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString()))
            .ToList();
        
        var safetyOptions = Enum.GetValues<SafetyOptions>()
            .Where(x => x != 0)
            .Select(x => new LookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString()))
            .ToList();
        
        var multimediaOptions = Enum.GetValues<MultimediaOptions>()
            .Where(x => x != 0)
            .Select(x => new LookupResponseDto(
                (int)x, 
                x.ToString(), 
                x.ToFriendlyString()))
            .ToList();

        return new LookupsResponseDto(
            transmissions,
            fuelTypes,
            euroStandards,
            driveTypes,
            colorShades,
            paintworkConditions,
            conditions,
            comfortOptions,
            safetyOptions,
            multimediaOptions);
    }
}