using Microsoft.Extensions.Hosting;

namespace GhostServiceBuster.IntegrationTests.Testees;

public sealed class TestHostedService(IService6 service) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _ = service;
        return Task.CompletedTask;
    }
}
