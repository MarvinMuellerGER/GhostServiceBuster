namespace GhostServiceBuster.NServiceBus;

public static class ServiceProviderExtensions
{
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        CreateServiceUsageVerifierForNServiceBusUnsafe(this IServiceProvider services) =>
        Verify.New.ForNServiceBusUnsafe(services);
}