using AutoriaClone.Api.Application.Responses.Auth;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Commands.Auth.Registration;

public record RegisterUserCommand(string Email, string Password) : IRequest<Result<AccessTokenResponseDto>>
{
    public class Handler : IRequestHandler<RegisterUserCommand, Result<AccessTokenResponseDto>>
    {
        private readonly IIdentityService _identityService;

        public Handler(IIdentityService identityService)
            => _identityService = identityService;

        public Task<Result<AccessTokenResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken) 
            => _identityService.RegisterUserAsync(request.Email, request.Password, cancellationToken);
    }
}