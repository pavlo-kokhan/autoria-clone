namespace AutoriaClone.Api.Middlewares;

public class AccessTokenMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Query.TryGetValue("accessToken", out var accessToken))
        {
            context.Request.Headers.Authorization = $"Bearer {accessToken}";
        }

        return next(context);
    }
}
