using System.Text.Json.Serialization;

namespace AutoriaClone.Api.Application.Services.Providers.RegionProvider.UkrPoshta;

public record CitiesResponse(
    [property: JsonPropertyName("Entries")] CityEntries Entries
);

public record CityEntries(
    [property: JsonPropertyName("Entry")] List<CityEntry> Entry
);

public record CityEntry(
    [property: JsonPropertyName("CITY_ID")] string? CityId,
    [property: JsonPropertyName("REGION_ID")] string? RegionId,
    [property: JsonPropertyName("DISTRICT_ID")] string? DistrictId,
    [property: JsonPropertyName("CITY_KATOTTG")] string? CityKatottg,
    [property: JsonPropertyName("CITY_KOATUU")] string? CityKoatuu,
    [property: JsonPropertyName("CITY_UA")] string? CityUa,
    [property: JsonPropertyName("REGION_UA")] string? RegionUa,
    [property: JsonPropertyName("DISTRICT_UA")] string? DistrictUa,
    [property: JsonPropertyName("CITYTYPE_UA")] string? CityTypeUa,
    [property: JsonPropertyName("SHORTCITYTYPE_UA")] string? ShortCityTypeUa,
    [property: JsonPropertyName("OLDCITY_UA")] string? OldCityUa,
    [property: JsonPropertyName("NEW_DISTRICT_UA")] string? NewDistrictUa,
    [property: JsonPropertyName("NAME_UA")] string? RecordStatus,
    [property: JsonPropertyName("OWNOF")] string? OwnOf,
    [property: JsonPropertyName("CITY_EN")] string? CityEn,
    [property: JsonPropertyName("REGION_EN")] string? RegionEn,
    [property: JsonPropertyName("DISTRICT_EN")] string? DistrictEn,
    [property: JsonPropertyName("CITYTYPE_EN")] string? CityTypeEn,
    [property: JsonPropertyName("SHORTCITYTYPE_EN")] string? ShortCityTypeEn,
    [property: JsonPropertyName("OLDCITY_EN")] string? OldCityEn,
    [property: JsonPropertyName("CITY_RU")] string? CityRu,
    [property: JsonPropertyName("REGION_RU")] string? RegionRu,
    [property: JsonPropertyName("DISTRICT_RU")] string? DistrictRu,
    [property: JsonPropertyName("CITYTYPE_RU")] string? CityTypeRu,
    [property: JsonPropertyName("SHORTCITYTYPE_RU")] string? ShortCityTypeRu,
    [property: JsonPropertyName("OLDCITY_RU")] string? OldCityRu,
    [property: JsonPropertyName("POPULATION")] string? Population,
    [property: JsonPropertyName("LATTITUDE")] string? Latitude,
    [property: JsonPropertyName("LONGITUDE")] string? Longitude,
    [property: JsonPropertyName("IS_DISTRICTCENTER")] string? IsDistrictCenter
);