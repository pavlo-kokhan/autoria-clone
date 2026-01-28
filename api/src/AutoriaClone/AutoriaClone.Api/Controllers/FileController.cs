using AutoriaClone.Api.Application.Commands.File;
using AutoriaClone.Api.Extensions;
using AutoriaClone.Api.Filters;
using AutoriaClone.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoriaClone.Api.Controllers;

[ApiController]
[Route("files")]
[AppAuthorize(Role.User)]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator) 
        => _mediator = mediator;

    [HttpPost("upload")]
    public async Task<IActionResult> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
        => (await _mediator.Send(new UploadFileCommand(file), cancellationToken)).ToActionResult();
}