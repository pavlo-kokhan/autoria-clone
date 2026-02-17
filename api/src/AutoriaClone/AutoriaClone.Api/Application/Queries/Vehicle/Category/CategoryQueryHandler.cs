using AutoriaClone.Api.Application.Responses.Vehicle.Category;
using AutoriaClone.Domain.Results.Generic;
using AutoriaClone.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoriaClone.Api.Application.Queries.Vehicle.Category;

public class CategoryQueryHandler : IRequestHandler<CategoriesQuery, Result<IReadOnlyCollection<CategoryResponseDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryQueryHandler(ApplicationDbContext dbContext) 
        => _dbContext = dbContext;

    public async Task<Result<IReadOnlyCollection<CategoryResponseDto>>> Handle(CategoriesQuery request, CancellationToken cancellationToken) 
        => await _dbContext
            .Categories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new CategoryResponseDto(x.Id, x.Name))
            .ToListAsync(cancellationToken);
}