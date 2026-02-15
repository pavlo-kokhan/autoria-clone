using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using AutoriaClone.Api.Application.Extensions;
using AutoriaClone.Api.Application.Services.Abstract;

namespace AutoriaClone.Api.Application.Services.Providers.RegionProvider.UkrPoshta;

public class UkrPoshtaRegionProvider : IUkrPoshtaRegionProvider
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    private readonly IHttpClientFactory _httpClientFactory;

    public UkrPoshtaRegionProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<RegionsResponse?> GetRegionsAsync(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateUkrPoshta();

        var request = new HttpRequestMessage(HttpMethod.Get, "address-classifier-ws/get_regions_by_region_ua")
        {
            Headers =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json)
                }
            }
        };

        var responseContent = await SendRequestAsync(client, request, cancellationToken);
        
        if (responseContent is null)
            return null;
        
        return JsonSerializer.Deserialize<RegionsResponse>(responseContent, JsonSerializerOptions);
    }
    
    public async Task<CitiesResponse?> GetCitiesAsync(string regionId, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateUkrPoshta();
        
        var request = new HttpRequestMessage(HttpMethod.Get, $"address-classifier-ws/get_city_by_region_id_and_district_id_and_city_ua?region_id={regionId}")
        {
            Headers =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json)
                }
            }
        };
        
        var responseContent = await SendRequestAsync(client, request, cancellationToken);
        
        if (responseContent is null)
            return null;
        
        return JsonSerializer.Deserialize<CitiesResponse>(responseContent, JsonSerializerOptions);
    }

    private async Task<string?> SendRequestAsync(HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await client.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}