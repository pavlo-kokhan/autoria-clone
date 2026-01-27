using AutoriaClone.Api.Application.Queries.Auth.AccessToken;
using AutoriaClone.Api.Application.Queries.Auth.RefreshToken;
using AutoriaClone.Api.Application.Queries.Auth.Registration;
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

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegistrationAccessTokenQuery request, CancellationToken cancellationToken = default)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();

    [HttpPost("access-token")]
    public async Task<IActionResult> GetTokenAsync(AccessTokenQuery request, CancellationToken cancellationToken = default)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenQuery request, CancellationToken cancellationToken = default)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
}