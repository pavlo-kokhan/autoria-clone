using AutoriaClone.Api.Application.Responses.File;
using AutoriaClone.Api.Application.Services;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain;
using AutoriaClone.Domain.Aggregates.Entities.File;
using AutoriaClone.Domain.Providers.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Commands.File;

public record UploadFileCommand(string FileName, string ContentType, long FileSize) : IRequest<Result<InitFileUploadResponseDto>>
{
    public class Handler : IRequestHandler<UploadFileCommand, Result<InitFileUploadResponseDto>>
    {
        private readonly IStorageService _storageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IUserProvider _userProvider;
        
        public Handler(IStorageService storageService, IUnitOfWork unitOfWork, IHostEnvironment hostEnvironment, IUserProvider userProvider)
        {
            _storageService = storageService;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _userProvider = userProvider;
        }

        public async Task<Result<InitFileUploadResponseDto>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var userId = _userProvider.Id;
            var fileName = Path.GetFileNameWithoutExtension(request.FileName);
            var fileExtension = Path.GetExtension(request.FileName);
            
            var key = BlobKeyBuilder
                .Create()
                .InFolder(_hostEnvironment.EnvironmentName)
                .WithOwner(userId)
                .WithName(fileName, fileExtension)
                .ToString();

            var createFileResult = FileEntity.Create(key, fileExtension, userId);

            if (createFileResult.IsFailure)
                return createFileResult.ToFailureResult<InitFileUploadResponseDto>();

            await _unitOfWork.FileRepository.CreateAsync(createFileResult.Data, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return  new InitFileUploadResponseDto(
                _storageService.GenerateWriteSasUrl(key, request.ContentType),
                createFileResult.Data.Id);
        }
    }
}