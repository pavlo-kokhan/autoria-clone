using AutoriaClone.Domain.Results.Abstract;

namespace AutoriaClone.Domain.Results.Generic.Abstract;

public interface IResult<out T> : IResult
{
    T Data { get; }
}