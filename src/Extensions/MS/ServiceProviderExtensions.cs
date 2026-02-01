namespace GhostServiceBuster.MS;

public static class ServiceProviderExtensions
{
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifierUnsafe(
        this IServiceProvider services) =>
        Verify.New.ForServiceProviderUnsafe(services);
}