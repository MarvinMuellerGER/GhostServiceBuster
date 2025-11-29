namespace GhostServiceBuster.AspNet;

public static class ServiceProviderExtensions
{
    extension(IServiceProvider services)
    {
        public IServiceUsageVerifier CreateServiceUsageVerifier() => Verify.New.ForAspNet(services);
    }
}