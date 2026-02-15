using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Responses.User;
using AutoriaClone.Domain;
using AutoriaClone.Domain.Providers.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.User;

public record UserQuery(int? Id) : IRequest<Result<UserResponseDto>>
{
    public class Handler : IRequestHandler<UserQuery, Result<UserResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<Result<UserResponseDto>> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var userId = request.Id ?? _userProvider.Id;
            
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);
            
            if (user is null)
                return UserValidationError.NotFound;
            
            return new UserResponseDto(
                user.Email!,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.TelegramUserName,
                user.WebSiteUrl,
                user.Address);
        }
    }
}