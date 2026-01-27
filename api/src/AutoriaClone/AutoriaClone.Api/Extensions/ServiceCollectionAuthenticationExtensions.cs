using System.Text;
using AutoriaClone.Api.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AutoriaClone.Api.Extensions;

public static class ServiceCollectionAuthenticationExtensions
{
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var jwtKey = configuration["JwtTokenOptions:Key"]!;
        
        return serviceCollection
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters.IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                options.TokenValidationParameters.ValidateLifetime = true;
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.ValidateIssuer = false;
                options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);
            })
            .Services
            .Configure<JwtTokenOptions>(options =>
            {
                options.Key = Encoding.UTF8.GetBytes(jwtKey);
                options.ExpiresIn = (int)TimeSpan.FromHours(1).TotalSeconds;
                options.RefreshTokenExpiresIn = (int)TimeSpan.FromDays(30).TotalSeconds;
            });
    }
}
