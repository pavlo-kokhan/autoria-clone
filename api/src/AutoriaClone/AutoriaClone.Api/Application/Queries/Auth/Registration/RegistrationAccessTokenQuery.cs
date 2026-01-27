using AutoriaClone.Api.Application.Responses.Auth;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Auth.Registration;

public record RegistrationAccessTokenQuery(string Email, string Password) : IRequest<Result<AccessTokenResponseDto>>
{
    public class Handler : IRequestHandler<RegistrationAccessTokenQuery, Result<AccessTokenResponseDto>>
    {
        private readonly IIdentityService _identityService;

        public Handler(IIdentityService identityService)
            => _identityService = identityService;

        public Task<Result<AccessTokenResponseDto>> Handle(RegistrationAccessTokenQuery request, CancellationToken cancellationToken)
            => _identityService.RegisterUserAsync(request.Email, request.Password, cancellationToken);
    }
}