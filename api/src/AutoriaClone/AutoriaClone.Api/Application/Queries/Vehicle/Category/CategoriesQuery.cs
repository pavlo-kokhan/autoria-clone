using AutoriaClone.Api.Application.Responses.Vehicle.Category;
using AutoriaClone.Domain.Results.Generic;
using MediatR;

namespace AutoriaClone.Api.Application.Queries.Vehicle.Category;

public record CategoriesQuery : IRequest<Result<IReadOnlyCollection<CategoryResponseDto>>>;