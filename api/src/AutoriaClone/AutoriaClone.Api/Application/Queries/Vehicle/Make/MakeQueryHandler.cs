using AutoriaClone.Api.Application.Responses.Vehicle.Make;
using AutoriaClone.Domain.Results.Generic;
using AutoriaClone.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoriaClone.Api.Application.Queries.Vehicle.Make;

public class MakeQueryHandler : IRequestHandler<MakesByCategoryIdQuery, Result<IReadOnlyCollection<MakeResponseDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public MakeQueryHandler(ApplicationDbContext dbContext) 
        => _dbContext = dbContext;

    public async Task<Result<IReadOnlyCollection<MakeResponseDto>>> Handle(MakesByCategoryIdQuery request, CancellationToken cancellationToken) 
        => await _dbContext
            .Makes
            .AsNoTracking()
            .Where(x => x.CategoryId == request.CategoryId)
            .OrderBy(x => x.Name)
            .Select(x => new MakeResponseDto(x.Id, x.Name))
            .ToListAsync(cancellationToken);
}