using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS;

public static class ServiceCollectionExtensions
{
    public static IServiceUsageVerifier CreateServiceUsageVerifier(this IServiceCollection services) =>
        Verify.New.ForServiceCollection(services);
}