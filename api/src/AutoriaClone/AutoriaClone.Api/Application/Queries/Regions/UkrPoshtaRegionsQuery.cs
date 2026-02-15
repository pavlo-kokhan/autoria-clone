using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Responses.Region;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Regions;

public record UkrPoshtaRegionsQuery : IRequest<Result<IReadOnlyCollection<UkrPoshtaRegionsResponseDto>>>
{
    public class Handler : IRequestHandler<UkrPoshtaRegionsQuery, Result<IReadOnlyCollection<UkrPoshtaRegionsResponseDto>>>
    {
        private readonly IUkrPoshtaRegionProvider _ukrPoshtaRegionProvider;

        public Handler(IUkrPoshtaRegionProvider ukrPoshtaRegionProvider) 
            => _ukrPoshtaRegionProvider = ukrPoshtaRegionProvider;

        public async Task<Result<IReadOnlyCollection<UkrPoshtaRegionsResponseDto>>> Handle(UkrPoshtaRegionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var regions = await _ukrPoshtaRegionProvider.GetRegionsAsync(cancellationToken);

                if (regions is null)
                    return RegionValidationErrors.RegionsNotFound;
                
                return regions.Entries.Entry
                    .Select(x => new UkrPoshtaRegionsResponseDto(x.RegionId, x.RegionUa, x.RegionEn))
                    .ToList();
            }
            catch (Exception)
            {
                return RegionValidationErrors.RegionsNotFound; 
            }
        }
    }
}