namespace AutoriaClone.Domain.Results;

public enum ResultStatus
{
    Ok,
    InvalidArgument,
    Forbidden,
    Unauthenticated,
    NotFound,
    InternalError,
    ApiError
}