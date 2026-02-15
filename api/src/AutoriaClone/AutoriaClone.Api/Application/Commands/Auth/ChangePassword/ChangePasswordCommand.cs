using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain;
using AutoriaClone.Domain.Providers.Abstract;
using AutoriaClone.Domain.Results;
using MediatR;

namespace AutoriaClone.Api.Application.Commands.Auth.ChangePassword;

public record ChangePasswordCommand(string Password, string NewPassword) : IRequest<Result>
{
    
    public class Handler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserProvider _userProvider;

        public Handler(IIdentityService identityService, IUserProvider userProvider)
        {
            _identityService = identityService;
            _userProvider = userProvider;
        }

        public Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken) 
            => _identityService.ChangePasswordAsync(_userProvider.Id, request.Password, request.NewPassword, cancellationToken);
    }
}