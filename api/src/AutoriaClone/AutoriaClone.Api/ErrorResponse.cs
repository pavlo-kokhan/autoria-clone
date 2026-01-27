using AutoriaClone.Domain.Results;

namespace AutoriaClone.Api;

public record ErrorResponse(IDictionary<string, string?> Errors, ResultStatus ResultStatus);
