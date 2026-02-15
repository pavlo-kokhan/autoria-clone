using AutoriaClone.Api.Application.Commands.Auth.ChangePassword;
using AutoriaClone.Api.Application.Commands.Auth.ConfirmationEmail;
using AutoriaClone.Api.Application.Commands.Auth.ConfirmEmail;
using AutoriaClone.Api.Application.Commands.Auth.Registration;
using AutoriaClone.Api.Application.Queries.Auth.AccessToken;
using AutoriaClone.Api.Application.Queries.Auth.RefreshToken;
using AutoriaClone.Api.Extensions;
using AutoriaClone.Api.Filters;
using AutoriaClone.Domain.Constants;
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
    public async Task<IActionResult> RegisterAsync(RegisterUserCommand request, CancellationToken cancellationToken = default)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPost("send-email-confirmation")]
    [AppAuthorize(Role.User)]
    public async Task<IActionResult> SendEmailConfirmationAsync(CancellationToken cancellationToken = default)
        => (await _mediator.Send(new SendConfirmationEmailCommand(), cancellationToken)).ToActionResult();
    
    [HttpPut("confirm-email")]
    [AppAuthorize(Role.User)]
    public async Task<IActionResult> ConfirmEmailAsync(ConfirmUserEmailCommand request, CancellationToken cancellationToken = default)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPut("change-password")]
    [AppAuthorize(Role.User)]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand request, CancellationToken cancellationToken = default)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();

    [HttpPost("access-token")]
    public async Task<IActionResult> GetTokenAsync(AccessTokenQuery request, CancellationToken cancellationToken = default)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenQuery request, CancellationToken cancellationToken = default)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
}