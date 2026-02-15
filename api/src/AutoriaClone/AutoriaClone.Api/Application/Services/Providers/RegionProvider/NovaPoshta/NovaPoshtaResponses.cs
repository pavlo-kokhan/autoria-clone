using System.Text.Json.Serialization;

namespace AutoriaClone.Api.Application.Services.Providers.RegionProvider.NovaPoshta;

// Обгортка для будь-якої відповіді НП
public record NovaPoshtaResponse<T>(
    [property: JsonPropertyName("success")] bool Success,
    [property: JsonPropertyName("data")] List<T> Data,
    [property: JsonPropertyName("errors")] List<string> Errors,
    [property: JsonPropertyName("info")] object Info
);

// Модель Області
public record NovaPoshtaArea(
    [property: JsonPropertyName("Ref")] string Ref,
    [property: JsonPropertyName("Description")] string Description,
    [property: JsonPropertyName("DescriptionRu")] string DescriptionRu,
    [property: JsonPropertyName("AreasCenter")] string AreasCenter
);

// Модель Міста
public record NovaPoshtaCity(
    [property: JsonPropertyName("Ref")] string Ref,
    [property: JsonPropertyName("Description")] string Description, // Напр: "Адамівка (Рівненська обл.)"
    [property: JsonPropertyName("DescriptionRu")] string DescriptionRu,
    [property: JsonPropertyName("Area")] string AreaRef, // Ref області
    [property: JsonPropertyName("AreaDescription")] string AreaDescription,
    [property: JsonPropertyName("SettlementTypeDescription")] string SettlementType, // "село", "місто"
    [property: JsonPropertyName("CityID")] string CityId
);