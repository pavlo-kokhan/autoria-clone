using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Domain;
using AutoriaClone.Domain.Aggregates.ValueObjects.Address;
using AutoriaClone.Domain.Providers.Abstract;
using AutoriaClone.Domain.Results;
using MediatR;

namespace AutoriaClone.Api.Application.Commands.User;

public record UpdateUserCommand(
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? TelegramUserName,
    string? WebSiteUrl,
    AddressValueObject? Address) : IRequest<Result>
{
    public class Handler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(_userProvider.Id, cancellationToken);

            if (user is null)
                return UserValidationError.NotFound;

            var updateResult = user.Update(
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.TelegramUserName,
                request.WebSiteUrl,
                request.Address);

            if (updateResult.IsFailure)
                return updateResult;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}