using AutoriaClone.Api.Application.Constants;

namespace AutoriaClone.Api.Application.Extensions;

public static class HttpClientFactoryExtensions
{
    public static HttpClient CreateUkrPoshta(this IHttpClientFactory factory)
        => factory.CreateClient(HttpClientNames.UkrPoshta);
    
    public static HttpClient CreateNovaPoshta(this IHttpClientFactory factory)
        => factory.CreateClient(HttpClientNames.NovaPoshta);
}