using AutoriaClone.Api.Application.Responses.Vehicle.Model;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Vehicle.Model;

public record ModelsByMakeIdQuery(int MakeId) : IRequest<Result<IReadOnlyCollection<ModelResponseDto>>>;