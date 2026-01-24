namespace GhostServiceBuster.MS;

public static class ServiceProviderExtensions
{
    public static IServiceUsageVerifier CreateServiceUsageVerifier(this IServiceProvider services) =>
        Verify.New.ForServiceProvider(services);
}