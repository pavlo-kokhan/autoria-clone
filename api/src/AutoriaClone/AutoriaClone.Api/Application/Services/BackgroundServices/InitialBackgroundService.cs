using AutoriaClone.Api.Application.Services.Abstract;

namespace AutoriaClone.Api.Application.Services.BackgroundServices;

public class InitialBackgroundService : IHostedService
{
    private readonly ILogger<InitialBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public InitialBackgroundService(ILogger<InitialBackgroundService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Initial background service started...");

        using var scope = _serviceProvider.CreateScope();
        var blobsConnectionVerifier = scope.ServiceProvider.GetRequiredService<IBlobsConnectionVerifier>();
        
        // fail fast for blob storage
        await blobsConnectionVerifier.CheckConnectionAsync();
        await blobsConnectionVerifier.EnsureContainersExistsAsync();
        
        _logger.LogInformation("Initial background service finished...");
    }

    public Task StopAsync(CancellationToken cancellationToken) 
        => Task.CompletedTask;
}