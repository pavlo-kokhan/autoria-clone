using AutoriaClone.Api.Application.Queries.Cities;
using AutoriaClone.Api.Application.Queries.Regions;
using AutoriaClone.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoriaClone.Api.Controllers;

[ApiController]
[Route("regions")]
public class RegionController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public RegionController(IMediator mediator) 
        => _mediator = mediator;

    [HttpGet("ukrposhta")]
    public async Task<IActionResult> GetRegionsUkrPoshtaAsync(CancellationToken cancellationToken)
        => (await _mediator.Send(new UkrPoshtaRegionsQuery(), cancellationToken)).ToActionResult();
    
    [HttpGet("ukrposhta/cities/{regionId}")]
    public async Task<IActionResult> GetCitiesUkrPoshtaAsync(string regionId, CancellationToken cancellationToken)
        => (await _mediator.Send(new UkrPoshtaCitiesQuery(regionId), cancellationToken)).ToActionResult();
    
    [HttpGet("novaposhta")]
    public async Task<IActionResult> GetRegionsNovaPoshtaAsync(CancellationToken cancellationToken)
        => (await _mediator.Send(new NovaPoshtaRegionsQuery(), cancellationToken)).ToActionResult();
    
    [HttpGet("novaposhta/cities/{regionRef}")]
    public async Task<IActionResult> GetCitiesNovaPoshtaAsync(string regionRef, int? limit, CancellationToken cancellationToken)
        => (await _mediator.Send(new NovaPoshtaCitiesQuery(regionRef, limit), cancellationToken)).ToActionResult();
}