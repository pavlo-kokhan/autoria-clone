using AutoriaClone.Api.Application.Services.Providers.RegionProvider;
using AutoriaClone.Api.Application.Services.Providers.RegionProvider.UkrPoshta;

namespace AutoriaClone.Api.Application.Services.Abstract;

public interface IUkrPoshtaRegionProvider
{
    Task<RegionsResponse?> GetRegionsAsync(CancellationToken cancellationToken = default);
    
    Task<CitiesResponse?> GetCitiesAsync(string regionId, CancellationToken cancellationToken = default);
}