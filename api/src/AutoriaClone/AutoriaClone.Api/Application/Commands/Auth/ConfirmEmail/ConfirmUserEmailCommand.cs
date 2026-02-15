using System.Text;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Providers.Abstract;
using AutoriaClone.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;

namespace AutoriaClone.Api.Application.Commands.Auth.ConfirmEmail;

public record ConfirmUserEmailCommand(string Token) : IRequest<Result>
{
    public class Handler : IRequestHandler<ConfirmUserEmailCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserProvider _userProvider;

        public Handler(IIdentityService identityService, IUserProvider userProvider)
        {
            _identityService = identityService;
            _userProvider = userProvider;
        }

        public Task<Result> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
        {
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            
            return _identityService.ConfirmUserEmailAsync(_userProvider.Id, decodedToken, cancellationToken);
        }
    }
}