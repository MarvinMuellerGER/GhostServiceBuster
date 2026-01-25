using GhostServiceBuster.Default;
using GhostServiceBuster.MS.Extract;
using GhostServiceBuster.MS.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS;

public static class ServiceUsageVerifierExtensions
{
    extension(IServiceUsageVerifier serviceUsageVerifier)
    {
        public IServiceUsageVerifier ForServiceProviderUnsafe(IServiceProvider services) =>
            serviceUsageVerifier.ForServiceCollection()
                .LazyRegisterAllServices(() => services);

        public IServiceUsageVerifier ForServiceCollection(IServiceCollection services)
        {
            return serviceUsageVerifier.ForServiceCollection()
                .LazyRegisterAllServices(() => services);
        }

        public IServiceUsageVerifier ForServiceCollection() =>
            serviceUsageVerifier.Default()
                .RegisterServiceCollectionServiceInfoExtractor()
                .RegisterServiceProviderUsageRootServicesFilter()
                .RegisterMicrosoftAndSystemNamespacesAllServicesFilter();
    }
}