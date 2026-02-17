using AutoriaClone.Api.Application.Responses.Vehicle.Generation;
using AutoriaClone.Domain.Results.Generic;
using AutoriaClone.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoriaClone.Api.Application.Queries.Vehicle.Generation;

public class GenerationQueryHandler : IRequestHandler<GenerationsByModelIdQuery, Result<IReadOnlyCollection<GenerationResponseDto>>>
{
    private readonly ApplicationDbContext _dbContext;
    
    public GenerationQueryHandler(ApplicationDbContext dbContext) 
        => _dbContext = dbContext;
    
    public async Task<Result<IReadOnlyCollection<GenerationResponseDto>>> Handle(GenerationsByModelIdQuery request, CancellationToken cancellationToken) 
        => await _dbContext
            .Generations
            .AsNoTracking()
            .Where(x => x.ModelId == request.ModelId)
            .OrderBy(x => x.YearFrom)
            .Select(x => new GenerationResponseDto(x.Id, x.Name, x.YearFrom, x.YearTo))
            .ToListAsync(cancellationToken);
}