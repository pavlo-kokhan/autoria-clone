using AutoriaClone.Api.Application.Responses.Vehicle.Make;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Vehicle.Make;

public record MakesByCategoryIdQuery(int CategoryId) : IRequest<Result<IReadOnlyCollection<MakeResponseDto>>>;