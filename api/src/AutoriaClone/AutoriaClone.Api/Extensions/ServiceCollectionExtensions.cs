using System.Reflection;
using AutoriaClone.Domain.Aggregates.Attributes;
using AutoriaClone.Domain.Aggregates.Entities.User;
using AutoriaClone.Domain.Aggregates.ValueObjects;
using AutoriaClone.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

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
            })
            .AddUserManager<UserManager<UserEntity>>()
            .AddRoles<IdentityRole<int>>()
            .AddRoleManager<RoleManager<IdentityRole<int>>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
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
}