using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Domain;
using AutoriaClone.Domain.Aggregates.Entities.User;
using AutoriaClone.Domain.Providers.Abstract;
using AutoriaClone.Domain.Results;
using MediatR;

namespace AutoriaClone.Api.Application.Commands.User;

public record UpdateUserContactsCommand(
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? TelegramUserName) : IRequest<Result>
{
    public class Handler : IRequestHandler<UpdateUserContactsCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<Result> Handle(UpdateUserContactsCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(_userProvider.Id, cancellationToken);

            if (user is null)
                return UserValidationError.NotFound;

            var updateResult = user.UpdateContacts(new UserContactsValueObject(
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.TelegramUserName));

            if (updateResult.IsFailure)
                return updateResult;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}