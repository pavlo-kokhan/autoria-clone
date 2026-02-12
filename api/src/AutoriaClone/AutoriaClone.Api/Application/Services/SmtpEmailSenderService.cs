using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results;
using FluentEmail.Core;

namespace AutoriaClone.Api.Application.Services;

public class SmtpEmailSenderService : IEmailSenderService
{
    private readonly IFluentEmail _fluentEmail;

    public SmtpEmailSenderService(IFluentEmail fluentEmail) 
        => _fluentEmail = fluentEmail;

    public async Task<Result> SendAsync(string toEmail, string subject, string body, CancellationToken cancellationToken = default)
    {
        var response = await _fluentEmail
            .To(toEmail)
            .Subject(subject)
            .Body(body)
            .SendAsync();
        
        return response.Successful ? Result.Success() : EmailValidationError.FailedToSend;
    }
}