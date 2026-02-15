using System.Text.Json.Serialization;

namespace AutoriaClone.Api.Application.Services.Providers.RegionProvider.UkrPoshta;

public record RegionsResponse(
    [property: JsonPropertyName("Entries")] RegionEntries Entries
);

public record RegionEntries(
    [property: JsonPropertyName("Entry")] List<RegionEntry> Entry
);

public record RegionEntry(
    [property: JsonPropertyName("REGION_ID")] string? RegionId,
    [property: JsonPropertyName("REGION_UA")] string? RegionUa,
    [property: JsonPropertyName("REGION_EN")] string? RegionEn,
    [property: JsonPropertyName("REGION_KATOTTG")] string? RegionKatottg,
    [property: JsonPropertyName("REGION_KOATUU")] string? RegionKoatuu
);