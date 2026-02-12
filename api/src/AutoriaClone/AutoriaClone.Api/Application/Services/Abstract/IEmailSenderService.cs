using AutoriaClone.Domain.Results;

namespace AutoriaClone.Api.Application.Services.Abstract;

public interface IEmailSenderService
{
    Task<Result> SendAsync(string toEmail, string subject, string body, CancellationToken cancellationToken = default);
}