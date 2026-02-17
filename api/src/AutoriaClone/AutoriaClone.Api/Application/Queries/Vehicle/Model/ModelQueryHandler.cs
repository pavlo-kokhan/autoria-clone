using AutoriaClone.Api.Application.Responses.Vehicle.Model;
using AutoriaClone.Domain.Results.Generic;
using AutoriaClone.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoriaClone.Api.Application.Queries.Vehicle.Model;

public class ModelQueryHandler : IRequestHandler<ModelsByMakeIdQuery, Result<IReadOnlyCollection<ModelResponseDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public ModelQueryHandler(ApplicationDbContext dbContext) 
        => _dbContext = dbContext;
    
    public async Task<Result<IReadOnlyCollection<ModelResponseDto>>> Handle(ModelsByMakeIdQuery request, CancellationToken cancellationToken) 
        => await _dbContext
            .Models
            .AsNoTracking()
            .Where(x => x.MakeId == request.MakeId)
            .OrderBy(x => x.Name)
            .Select(x => new ModelResponseDto(x.Id, x.Name))
            .ToListAsync(cancellationToken);
}