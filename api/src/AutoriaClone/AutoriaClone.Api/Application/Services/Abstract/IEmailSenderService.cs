using AutoriaClone.Domain.Results;

namespace AutoriaClone.Api.Application.Services.Abstract;

public interface IEmailSenderService
{
    Task<Result> SendEmailConfirmationAsync(string toEmail, string token, int userId, CancellationToken cancellationToken = default);
}