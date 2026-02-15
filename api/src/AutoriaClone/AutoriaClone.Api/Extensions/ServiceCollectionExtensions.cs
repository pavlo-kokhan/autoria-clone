using System.Reflection;
using AutoriaClone.Api.Application.Options;
using AutoriaClone.Api.Application.Services;
using AutoriaClone.Api.Application.Services.Abstract;
using AutoriaClone.Domain.Aggregates.Attributes;
using AutoriaClone.Domain.Aggregates.Entities.User;
using AutoriaClone.Domain.Aggregates.ValueObjects;
using AutoriaClone.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Azure;

namespace AutoriaClone.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddIdentityCore<UserEntity>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 2;
                
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
            })
            .AddUserManager<UserManager<UserEntity>>()
            .AddRoles<IdentityRole<int>>()
            .AddRoleManager<RoleManager<IdentityRole<int>>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .Services;
    
    public static IServiceCollection AddFluentValidation(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddFluentValidationAutoValidation(configuration =>
            {
                configuration.DisableDataAnnotationsValidation = true;
            })
            .AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()], ServiceLifetime.Singleton)
            .AddValueObjectValidators();
    
    private static IServiceCollection AddValueObjectValidators(this IServiceCollection serviceCollection)
        => serviceCollection
            .Scan(selector => selector
                .FromAssemblyOf<MarkerValueObjectValidator>()
                .AddClasses(filter => filter.AssignableTo(typeof(IValidator<>)).WithAttribute<ValueObjectValidatorAttribute>())
                .As(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
                .WithSingletonLifetime());

    public static IServiceCollection AddAzureBlobServiceClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAzureClients(builder => 
        {
            builder.AddBlobServiceClient(configuration["AzureStorageOptions:ConnectionString"]!);
        });

        return serviceCollection
            .Configure<AzureStorageOptions>(configuration.GetSection(AzureStorageOptions.SectionName));
    }

    public static IServiceCollection AddEmailService(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddScoped<IEmailSenderService, AzureEmailSenderService>()
            .Configure<AzureCommunicationServicesOptions>(configuration.GetSection(AzureCommunicationServicesOptions.SectionName));
    
    public static IServiceCollection AddBaseUrlOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .Configure<BaseUrlOptions>(configuration.GetSection(BaseUrlOptions.SectionName));
}