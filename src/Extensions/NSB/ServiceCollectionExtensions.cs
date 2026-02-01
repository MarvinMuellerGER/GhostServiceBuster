using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.NServiceBus;

public static class ServiceCollectionExtensions
{
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifierForNServiceBus(
        this IServiceCollection services) =>
        Verify.New.ForNServiceBus(services);
}