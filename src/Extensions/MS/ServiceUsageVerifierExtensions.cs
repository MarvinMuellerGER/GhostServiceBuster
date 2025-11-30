using GhostServiceBuster.Default;
using GhostServiceBuster.MS.Extract;
using GhostServiceBuster.MS.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS;

public static class ServiceUsageVerifierExtensions
{
    extension(IServiceUsageVerifier serviceUsageVerifier)
    {
        public IServiceUsageVerifier ForServiceCollection(IServiceProvider services) =>
            serviceUsageVerifier.ForServiceCollection()
                .RegisterAllServices(services);

        public IServiceUsageVerifier ForServiceCollection(IServiceCollection services) =>
            serviceUsageVerifier.ForServiceCollection()
                .RegisterAllServices(services);

        public IServiceUsageVerifier ForServiceCollection() =>
            serviceUsageVerifier.Default()
                .RegisterServiceCollectionServiceInfoExtractor()
                .RegisterServiceProviderUsageRootServicesFilter()
                .UseAllServicesAsRootServices();
    }
}