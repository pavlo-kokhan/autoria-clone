using AutoriaClone.Api.Application.Responses.Vehicle.Generation;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Vehicle.Generation;

public record GenerationsByModelIdQuery(int ModelId) : IRequest<Result<IReadOnlyCollection<GenerationResponseDto>>>;