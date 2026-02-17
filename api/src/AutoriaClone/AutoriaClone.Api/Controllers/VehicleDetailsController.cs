using AutoriaClone.Api.Application.Helpers;
using AutoriaClone.Api.Application.Queries.Vehicle.Category;
using AutoriaClone.Api.Application.Queries.Vehicle.Generation;
using AutoriaClone.Api.Application.Queries.Vehicle.Make;
using AutoriaClone.Api.Application.Queries.Vehicle.Model;
using AutoriaClone.Api.Application.Responses.Vehicle.Lookup;
using AutoriaClone.Api.Extensions;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement.Flags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoriaClone.Api.Controllers;

[ApiController]
[Route("vehicle-details")]
public class VehicleDetailsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleDetailsController(IMediator mediator) 
        => _mediator = mediator;

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoriesAsync(CancellationToken cancellationToken)
        => (await _mediator.Send(new CategoriesQuery(), cancellationToken)).ToActionResult();

    [HttpGet("makes/{categoryId}")]
    public async Task<IActionResult> GetMakesAsync(int categoryId, CancellationToken cancellationToken)
        => (await _mediator.Send(new MakesByCategoryIdQuery(categoryId), cancellationToken)).ToActionResult();

    [HttpGet("models/{makeId}")]
    public async Task<IActionResult> GetModelsAsync(int makeId, CancellationToken cancellationToken)
        => (await _mediator.Send(new ModelsByMakeIdQuery(makeId), cancellationToken)).ToActionResult();

    [HttpGet("generations/{modelId}")]
    public async Task<IActionResult> GetGenerationsAsync(int modelId, CancellationToken cancellationToken)
        => (await _mediator.Send(new GenerationsByModelIdQuery(modelId), cancellationToken)).ToActionResult();

    [HttpGet("lookups")]
    public ActionResult<LookupsResponseDto> GetLookups() 
        => Ok(LookupHelper.GetLookups());
}