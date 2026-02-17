namespace AutoriaClone.Api.Application.Responses.Vehicle.Lookup;

public record LookupsResponseDto(
    IReadOnlyCollection<LookupResponseDto> Transmissions,
    IReadOnlyCollection<LookupResponseDto> FuelTypes,
    IReadOnlyCollection<LookupResponseDto> EuroStandards,
    IReadOnlyCollection<LookupResponseDto> DriveTypes,
    IReadOnlyCollection<ColorLookupResponseDto> ColorShades,
    IReadOnlyCollection<DescriptionLookupResponseDto> PaintworkConditions,
    IReadOnlyCollection<DescriptionLookupResponseDto> Conditions,
    IReadOnlyCollection<LookupResponseDto> ComfortOptionsFlags,
    IReadOnlyCollection<LookupResponseDto> SafetyOptionsFlags,
    IReadOnlyCollection<LookupResponseDto> MultimediaOptionsFlags);