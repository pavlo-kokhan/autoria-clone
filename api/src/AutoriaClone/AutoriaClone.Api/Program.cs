using System.Reflection;
using System.Text.Json.Serialization;
using AutoriaClone.Api.Application.Services;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Api.Extensions;
using AutoriaClone.Api.Filters;
using AutoriaClone.Api.Middlewares;
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
    .AddControllers(options =>
    {
        options.Filters.Add<ResultableActionFilterAttribute>();
        options.Filters.Add<ModelValidationActionFilterAttribute>();
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .Services
    .AddCors()
    .AddOpenApi()
    .AddScoped<AccessTokenMiddleware>()
    .AddScoped<IIdentityService, IdentityService>();

var app = builder.Build();

app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());
app.UseMiddleware<AccessTokenMiddleware>();

if (app.Environment.IsDebug() || app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .AddPreferredSecuritySchemes(AuthenticationScheme)
            .AddHttpAuthentication(AuthenticationScheme, scheme => scheme.Token = "{your_token_here}");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();

await app.RunAsync();