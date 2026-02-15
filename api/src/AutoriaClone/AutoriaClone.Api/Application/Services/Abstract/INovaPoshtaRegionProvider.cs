using AutoriaClone.Api.Application.Services.Providers.RegionProvider.NovaPoshta;

namespace AutoriaClone.Api.Application.Services.Abstract;

public interface INovaPoshtaRegionProvider
{
    Task<NovaPoshtaResponse<NovaPoshtaArea>?> GetRegionsAsync(CancellationToken cancellationToken = default);

    Task<NovaPoshtaResponse<NovaPoshtaCity>?> GetCitiesAsync(string areaRef, int? limit = null, CancellationToken cancellationToken = default);
}