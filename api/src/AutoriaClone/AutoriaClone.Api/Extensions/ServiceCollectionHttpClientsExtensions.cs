using AutoriaClone.Api.Application.Constants;
using AutoriaClone.Api.Application.Options;

namespace AutoriaClone.Api.Extensions;

public static class ServiceCollectionHttpClientsExtensions
{
    public static IServiceCollection AddUkrPoshtaHttpClient(this IServiceCollection serviceCollection, IConfiguration configuration) 
        => serviceCollection.AddHttpClient(HttpClientNames.UkrPoshta, client =>
            {
                client.BaseAddress = new Uri(configuration["UkrPoshtaOptions:BaseUrl"]!);
            })
            .Services;
    
    public static IServiceCollection AddNovaPoshtaHttpClient(this IServiceCollection serviceCollection, IConfiguration configuration) 
        => serviceCollection.AddHttpClient(HttpClientNames.NovaPoshta, client =>
            {
                client.BaseAddress = new Uri(configuration["NovaPoshtaOptions:BaseUrl"]!);
            })
            .Services
            .Configure<NovaPoshtaOptions>(configuration.GetSection(NovaPoshtaOptions.SectionName));
}