using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.AspNet;

public static class ServiceProviderExtensions
{
    extension(IServiceProvider serviceProvider)
    {
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifier(
            IServiceCollection services) =>
            Verify.New.ForAspNet(serviceProvider, services);

        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifierUnsafe() =>
            Verify.New.ForAspNetUnsafe(serviceProvider);
    }
}