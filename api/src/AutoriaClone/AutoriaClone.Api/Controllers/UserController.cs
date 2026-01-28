using AutoriaClone.Api.Application.Commands.User;
using AutoriaClone.Api.Application.Queries.User;
using AutoriaClone.Api.Extensions;
using AutoriaClone.Api.Filters;
using AutoriaClone.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoriaClone.Api.Controllers;

[ApiController]
[Route("user")]
[AppAuthorize(Role.User)]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator) 
        => _mediator = mediator;
    
    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        => (await _mediator.Send(new UserQuery(), cancellationToken)).ToActionResult();
    
    [HttpPut("contacts")]
    public async Task<IActionResult> UpdateContactsAsync(UpdateUserContactsCommand request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
}