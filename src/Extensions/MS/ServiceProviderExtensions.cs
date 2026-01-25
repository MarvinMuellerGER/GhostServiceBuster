namespace GhostServiceBuster.MS;

public static class ServiceProviderExtensions
{
    public static IServiceUsageVerifier CreateServiceUsageVerifierUnsafe(this IServiceProvider services) =>
        Verify.New.ForServiceProviderUnsafe(services);
}