using System.Text;
using AutoriaClone.Api.Application.Constants.ValidationErrors;
using AutoriaClone.Api.Application.Options;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Results;
using Azure;
using Azure.Communication.Email;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace AutoriaClone.Api.Application.Services;

public class AzureEmailSenderService : IEmailSenderService
{
    private readonly EmailClient _emailClient;
    private readonly AzureCommunicationServicesOptions _options;
    private readonly BaseUrlOptions _baseUrlOptions;

    public AzureEmailSenderService(IOptions<AzureCommunicationServicesOptions> options, IOptions<BaseUrlOptions> baseUrlOptions)
    {
        _options = options.Value;
        _baseUrlOptions = baseUrlOptions.Value;
        _emailClient = new EmailClient(_options.ConnectionString);
    }

    public async Task<Result> SendEmailConfirmationAsync(string toEmail, string token, int userId, CancellationToken cancellationToken = default)
    {
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        
        var queryParameters = new Dictionary<string, string>
        {
            { "id", userId.ToString() },
            { "token", encodedToken }
        };
        
        var confirmationUrl = QueryHelpers.AddQueryString(_baseUrlOptions.ClientBaseUrl, queryParameters!);
        
        var body = $"""
                    <html>
                      <body>
                        <h1>You are one step away from confirming your email!</h1>
                        <p><a href="{confirmationUrl}">Click to confirm</a></p>
                        <p>If you did not register, simply ignore this email.</p>
                      </body>
                    </html>
                    """;
        
        return await SendAsync(toEmail, "Autoria Clone email confirmation", body, cancellationToken);
    }
    
    private async Task<Result> SendAsync(string toEmail, string subject, string body, CancellationToken cancellationToken = default)
    {
        var message = new EmailMessage(
            senderAddress: _options.SenderEmail,
            content: new EmailContent(subject)
            {
                Html = body
            },
            recipients: new EmailRecipients([new EmailAddress(toEmail)]));
        
        var response = await _emailClient.SendAsync(WaitUntil.Completed, message, cancellationToken);

        if (!response.HasValue)
            return EmailValidationError.FailedToSend;
        
        return response.Value.Status == EmailSendStatus.Succeeded ? Result.Success() : EmailValidationError.FailedToSend;
    }
}