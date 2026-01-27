using System.Data.Common;
using AutoriaClone.Domain;
using AutoriaClone.Domain.Aggregates.Entities.User;
using AutoriaClone.Infrastructure;
using AutoriaClone.Infrastructure.Persistence;
using AutoriaClone.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using static Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId;

namespace AutoriaClone.Api.Extensions;

public static class ServiceCollectionDbExtensions
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddDbContext<ApplicationDbContext>(
                (sp, o) =>
                {
                    o.UseNpgsql(BuildSource(configuration.GetConnectionString(nameof(ApplicationDbContext))!));
                    o.ConfigureWarnings(builder => builder.Ignore(PendingModelChangesWarning));

                    if (sp.GetRequiredService<IHostEnvironment>().IsDebug())
                    {
                        o
                            .UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>())
                            .EnableSensitiveDataLogging();
                    }
                });

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IUserRepository, UserRepository>();
    
    private static DbDataSource BuildSource(string connectionString)
        => new NpgsqlDataSourceBuilder(connectionString)
        .EnableDynamicJson()
        .Build();
}
