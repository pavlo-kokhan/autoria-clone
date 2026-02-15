using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Responses.City;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Cities;

public record NovaPoshtaCitiesQuery(string RegionRef, int? Limit = null) : IRequest<Result<IReadOnlyCollection<NovaPoshtaCitiesResponseDto>>>
{
    public class Handler : IRequestHandler<NovaPoshtaCitiesQuery, Result<IReadOnlyCollection<NovaPoshtaCitiesResponseDto>>>
    {
        private readonly INovaPoshtaRegionProvider _novaPoshtaRegionProvider;
        
        public Handler(INovaPoshtaRegionProvider novaPoshtaRegionProvider) 
            => _novaPoshtaRegionProvider = novaPoshtaRegionProvider;
        
        public async Task<Result<IReadOnlyCollection<NovaPoshtaCitiesResponseDto>>> Handle(NovaPoshtaCitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _novaPoshtaRegionProvider.GetCitiesAsync(request.RegionRef, request.Limit, cancellationToken);
                
                if (response is null || !response.Success)
                    return RegionValidationErrors.CitiesNotFound;

                return response.Data
                    .Select(x => new NovaPoshtaCitiesResponseDto(
                        x.Ref,
                        x.AreaRef,
                        x.Description,
                        x.AreaDescription,
                        x.SettlementType,
                        $"{x.Description} {x.SettlementType}"))
                    .ToList();
            }
            catch (Exception)
            {
                return RegionValidationErrors.CitiesNotFound;
            }
        }
    }
}