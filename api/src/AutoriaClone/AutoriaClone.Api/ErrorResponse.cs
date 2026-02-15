using AutoriaClone.Domain.Results;

namespace AutoriaClone.Api;

public record ErrorResponse(IDictionary<string, string?> Errors, ResultStatus ResultStatus)
{
    public static readonly ErrorResponse UnhandledExceptionError = new(
        new Dictionary<string, string?>
        {
            {
                "Server", 
                "An unexpected error occurred."
            }
        }, 
        ResultStatus.InternalError);
}
