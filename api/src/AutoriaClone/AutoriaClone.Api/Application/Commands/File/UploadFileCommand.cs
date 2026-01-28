using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Responses.File;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain;
using AutoriaClone.Domain.Aggregates.Entities.File;
using AutoriaClone.Domain.Providers.Abstract;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Commands.File;

public record UploadFileCommand(IFormFile File) : IRequest<Result<UploadFileResponseDto>>
{
    public class Handler : IRequestHandler<UploadFileCommand, Result<UploadFileResponseDto>>
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IUserProvider _userProvider;
        
        public Handler(IFileService fileService, IUnitOfWork unitOfWork, IHostEnvironment hostEnvironment, IUserProvider userProvider)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _userProvider = userProvider;
        }

        public async Task<Result<UploadFileResponseDto>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var extension = new FileInfo(request.File.FileName).Extension;
            var userId = _userProvider.Id;
            var key = BuildBlobKey(userId, extension);

            var fileCreateResult = FileEntity.Create(key, extension, userId);

            if (fileCreateResult.IsFailure)
                return fileCreateResult.ToFailureResult<UploadFileResponseDto>();
            
            var uploadFileInfo = await _fileService.UploadAsync(key, request.File, cancellationToken);

            if (uploadFileInfo is null)
                return FileValidationError.FailedToUpload;

            await _unitOfWork.FileRepository.CreateAsync(fileCreateResult.Data, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UploadFileResponseDto(fileCreateResult.Data.Id, uploadFileInfo.SasUrl);
        }
        
        private string BuildBlobKey(int userId, string extension)
            => $"{_hostEnvironment.EnvironmentName}/{userId}/{Guid.NewGuid():N}{extension}";
    }
}