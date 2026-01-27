using AutoriaClone.Api.Application.Queries.Auth.AccessToken;
using AutoriaClone.Api.Application.Queries.Auth.RefreshToken;
using AutoriaClone.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoriaClone.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(AccessTokenQuery request, CancellationToken cancellation)
        => (await _mediator.Send(request, cancellation)).ToActionResult();

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenQuery request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
}