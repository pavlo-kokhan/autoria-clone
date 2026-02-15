using System.Net.Mime;
using System.Text;
using System.Text.Json;
using AutoriaClone.Api.Application.Extensions;
using AutoriaClone.Api.Application.Options;
using AutoriaClone.Api.Application.Services.Abstract;
using Microsoft.Extensions.Options;

namespace AutoriaClone.Api.Application.Services.Providers.RegionProvider.NovaPoshta;

public class NovaPoshtaRegionProvider : INovaPoshtaRegionProvider
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly NovaPoshtaOptions _options;
    
    public NovaPoshtaRegionProvider(IHttpClientFactory httpClientFactory, IOptions<NovaPoshtaOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task<NovaPoshtaResponse<NovaPoshtaArea>?> GetRegionsAsync(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateNovaPoshta();

        var body = new NovaPoshtaRequest(
            _options.ApiKey,
            "Address",
            "getAreas",
            new { }
        );

        var request = new HttpRequestMessage(HttpMethod.Post, string.Empty)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, MediaTypeNames.Application.Json)
        };
        
        return await SendRequestAsync<NovaPoshtaArea>(client, request, cancellationToken);
    }

    public async Task<NovaPoshtaResponse<NovaPoshtaCity>?> GetCitiesAsync(string areaRef, int? limit = null, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateNovaPoshta();

        object properties = limit is null ? new { AreaRef = areaRef } : new { AreaRef = areaRef, Limit = limit };
        
        var body = new NovaPoshtaRequest(
            _options.ApiKey,
            "Address",
            "getCities",
            properties
        );
        
        var request = new HttpRequestMessage(HttpMethod.Post, string.Empty)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, MediaTypeNames.Application.Json)
        };

        return await SendRequestAsync<NovaPoshtaCity>(client, request, cancellationToken);
    }

    private async Task<NovaPoshtaResponse<T>?> SendRequestAsync<T>(HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await client.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        
        return JsonSerializer.Deserialize<NovaPoshtaResponse<T>>(content, JsonSerializerOptions);
    }
}