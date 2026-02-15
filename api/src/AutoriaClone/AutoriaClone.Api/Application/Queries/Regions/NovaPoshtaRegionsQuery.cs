using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Responses.Region;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Regions;

public record NovaPoshtaRegionsQuery : IRequest<Result<IReadOnlyCollection<NovaPoshtaRegionsResponseDto>>>
{
    public class Handler : IRequestHandler<NovaPoshtaRegionsQuery, Result<IReadOnlyCollection<NovaPoshtaRegionsResponseDto>>>
    {
        private readonly INovaPoshtaRegionProvider _novaPoshtaRegionProvider;

        public Handler(INovaPoshtaRegionProvider novaPoshtaRegionProvider) 
            => _novaPoshtaRegionProvider = novaPoshtaRegionProvider;

        public async Task<Result<IReadOnlyCollection<NovaPoshtaRegionsResponseDto>>> Handle(NovaPoshtaRegionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _novaPoshtaRegionProvider.GetRegionsAsync(cancellationToken);
                
                if (response is null || !response.Success)
                    return RegionValidationErrors.RegionsNotFound;

                return response.Data
                    .Select(x => new NovaPoshtaRegionsResponseDto(x.Ref, x.Description))
                    .ToList();
            }
            catch (Exception)
            {
                return RegionValidationErrors.RegionsNotFound;
            }
        }
    }
}