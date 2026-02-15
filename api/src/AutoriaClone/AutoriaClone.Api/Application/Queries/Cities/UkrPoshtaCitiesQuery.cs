using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Responses.City;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Cities;

public record UkrPoshtaCitiesQuery(string RegionId) : IRequest<Result<IReadOnlyCollection<UkrPoshtaCitiesResponseDto>>>
{
    public class Handler : IRequestHandler<UkrPoshtaCitiesQuery, Result<IReadOnlyCollection<UkrPoshtaCitiesResponseDto>>>
    {
        private readonly IUkrPoshtaRegionProvider _ukrPoshtaRegionProvider;

        public Handler(IUkrPoshtaRegionProvider ukrPoshtaRegionProvider) 
            => _ukrPoshtaRegionProvider = ukrPoshtaRegionProvider;

        public async Task<Result<IReadOnlyCollection<UkrPoshtaCitiesResponseDto>>> Handle(UkrPoshtaCitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cities = await _ukrPoshtaRegionProvider.GetCitiesAsync(request.RegionId, cancellationToken);

                if (cities is null)
                    return RegionValidationErrors.CitiesNotFound;

                return cities.Entries.Entry
                    .Select(x => new UkrPoshtaCitiesResponseDto(x.CityId, x.RegionId, x.RegionUa, x.RegionEn, x.CityUa, x.CityEn))
                    .ToList();
            }
            catch (Exception)
            {
                return RegionValidationErrors.CitiesNotFound; 
            }
        }
    }
}