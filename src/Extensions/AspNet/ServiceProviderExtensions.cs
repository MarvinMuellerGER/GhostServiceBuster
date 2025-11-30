namespace GhostServiceBuster.AspNet;

public static class ServiceProviderExtensions
{
    public static IServiceUsageVerifier CreateServiceUsageVerifier(this IServiceProvider services) =>
        Verify.New.ForAspNet(services);
}