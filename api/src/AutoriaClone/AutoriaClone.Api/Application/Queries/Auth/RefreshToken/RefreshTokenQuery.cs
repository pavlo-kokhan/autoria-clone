using AutoriaClone.Api.Application.Responses.Auth;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Auth.RefreshToken;

public record RefreshTokenQuery(string RefreshToken) : IRequest<Result<AccessTokenResponseDto>>
{
    public class Handler : IRequestHandler<RefreshTokenQuery, Result<AccessTokenResponseDto>>
    {
        private readonly IIdentityService _identityService;

        public Handler(IIdentityService identityService)
            => _identityService = identityService;

        public Task<Result<AccessTokenResponseDto>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
            => _identityService.GetAccessTokenAsync(request.RefreshToken, cancellationToken);
    }
}
