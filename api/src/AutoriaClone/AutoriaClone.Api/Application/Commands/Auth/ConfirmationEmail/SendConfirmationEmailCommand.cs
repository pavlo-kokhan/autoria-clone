using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Providers.Abstract;
using AutoriaClone.Domain.Results;
using MediatR;

namespace AutoriaClone.Api.Application.Commands.Auth.ConfirmationEmail;

public record SendConfirmationEmailCommand : IRequest<Result>
{
    
    public class Handler : IRequestHandler<SendConfirmationEmailCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserProvider _userProvider;

        public Handler(IIdentityService identityService, IUserProvider userProvider)
        {
            _identityService = identityService;
            _userProvider = userProvider;
        }

        public Task<Result> Handle(SendConfirmationEmailCommand request, CancellationToken cancellationToken) 
            => _identityService.SendConfirmationEmailAsync(_userProvider.Id, cancellationToken);
    }
}