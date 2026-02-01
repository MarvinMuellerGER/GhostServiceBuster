using GhostServiceBuster.Default;
using GhostServiceBuster.MS.Extract;
using GhostServiceBuster.MS.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS;

public static class ServiceUsageVerifierExtensions
{
    extension(IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier)
    {
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForServiceProviderUnsafe(
            IServiceProvider services) =>
            serviceUsageVerifier.ForServiceCollection()
                .LazyRegisterAllServices(() => services);

        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForServiceCollection(
            IServiceCollection services) =>
            serviceUsageVerifier.ForServiceCollection()
                .LazyRegisterAllServices(() => services);

        public IServiceUsageVerifierWithCachedFiltersMutable ForServiceCollection() =>
            serviceUsageVerifier.Default()
                .RegisterServiceCollectionServiceInfoExtractor()
                .RegisterServiceProviderUsageRootServicesFilter()
                .RegisterMicrosoftAndSystemNamespacesAllServicesFilter();
    }

    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForServiceCollection(
        this IServiceUsageVerifierWithCachedServicesMutable serviceUsageVerifier) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)
        ((IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier).ForServiceCollection();
}