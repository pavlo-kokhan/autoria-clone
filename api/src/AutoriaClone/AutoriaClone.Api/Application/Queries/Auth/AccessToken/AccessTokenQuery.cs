using AutoriaClone.Api.Application.Responses.Auth;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Auth.AccessToken;

public record AccessTokenQuery(string Email, string Password) : IRequest<Result<AccessTokenResponseDto>>
{
    public class Handler : IRequestHandler<AccessTokenQuery, Result<AccessTokenResponseDto>>
    {
        private readonly IIdentityService _identityService;

        public Handler(IIdentityService identityService)
            => _identityService = identityService;

        public Task<Result<AccessTokenResponseDto>> Handle(AccessTokenQuery request, CancellationToken cancellationToken)
            => _identityService.GetAccessTokenAsync(request.Email, request.Password, cancellationToken);
    }
}
