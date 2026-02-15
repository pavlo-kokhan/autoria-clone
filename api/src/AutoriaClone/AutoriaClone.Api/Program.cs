using System.Reflection;
using System.Text.Json.Serialization;
using AutoriaClone.Api;
using AutoriaClone.Api.Application.Services;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Api.Application.Services.BackgroundServices;
using AutoriaClone.Api.Application.Services.File;
using AutoriaClone.Api.Application.Services.Identity;
using AutoriaClone.Api.Application.Services.Providers;
using AutoriaClone.Api.Application.Services.Providers.RegionProvider;
using AutoriaClone.Api.Application.Services.Providers.RegionProvider.NovaPoshta;
using AutoriaClone.Api.Application.Services.Providers.RegionProvider.UkrPoshta;
using AutoriaClone.Api.Extensions;
using AutoriaClone.Api.Filters;
using AutoriaClone.Api.Middlewares;
using AutoriaClone.Domain.Providers.Abstract;
using AutoriaClone.Infrastructure;
using AutoriaClone.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using static Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddIdentity()
    .AddAuthorization()
    .AddJwtBearerAuthentication(builder.Configuration)
    .AddFluentValidation()
    .AddMediatR(configuration =>
    {
        configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        configuration.Lifetime = ServiceLifetime.Scoped;
    })
    .AddPipelines()
    .AddDatabaseContext(builder.Configuration)
    .AddRepositories()
    .AddHttpContextAccessor()
    .AddEmailService(builder.Configuration)
    .AddBaseUrlOptions(builder.Configuration)
    .AddControllers(options =>
    {
        options.Filters.Add<ResultableActionFilterAttribute>();
        options.Filters.Add<ModelValidationActionFilterAttribute>();
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .Services
    .AddCors()
    .AddOpenApi("v1", options => options.AddDocumentTransformer<BearerSecuritySchemeTransformer>())
    .AddScoped<ExceptionHandlerMiddleware>()
    .AddScoped<AccessTokenMiddleware>()
    .AddScoped<IIdentityService, IdentityService>()
    .AddScoped<IUserProvider, UserProvider>()
    .AddScoped<DatabaseSeeder>()
    .AddAzureBlobServiceClient(builder.Configuration)
    .AddScoped<IBlobsConnectionVerifier, BlobsConnectionVerifier>()
    .AddHostedService<InitialBackgroundService>()
    .AddSingleton<IStorageService, AzureStorageService>()
    .AddScoped<IEmailSenderService, AzureEmailSenderService>()
    .AddUkrPoshtaHttpClient(builder.Configuration)
    .AddScoped<IUkrPoshtaRegionProvider, UkrPoshtaRegionProvider>()
    .AddNovaPoshtaHttpClient(builder.Configuration)
    .AddScoped<INovaPoshtaRegionProvider, NovaPoshtaRegionProvider>();

var app = builder.Build();

app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<AccessTokenMiddleware>();

if (app.Environment.IsDebug() || app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithPreferredScheme(AuthenticationScheme)
            .AddHttpAuthentication(AuthenticationScheme, scheme => scheme.Token = "{your_token_here}");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
await scope.ServiceProvider.GetRequiredService<DatabaseSeeder>().SeedAsync();
await scope.ServiceProvider.GetRequiredService<INovaPoshtaRegionProvider>().GetRegionsAsync();
await scope.ServiceProvider.GetRequiredService<INovaPoshtaRegionProvider>().GetCitiesAsync("71508138-9b87-11de-822f-000c2965ae0e");

await app.RunAsync();