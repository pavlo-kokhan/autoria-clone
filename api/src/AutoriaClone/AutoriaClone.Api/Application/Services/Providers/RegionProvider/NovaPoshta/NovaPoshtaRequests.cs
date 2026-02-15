using System.Text.Json.Serialization;

namespace AutoriaClone.Api.Application.Services.Providers.RegionProvider.NovaPoshta;

// Модель для тіла запиту (Request Body)
public record NovaPoshtaRequest(
    [property: JsonPropertyName("apiKey")] string ApiKey,
    [property: JsonPropertyName("modelName")] string ModelName,
    [property: JsonPropertyName("calledMethod")] string CalledMethod,
    [property: JsonPropertyName("methodProperties")] object MethodProperties
);