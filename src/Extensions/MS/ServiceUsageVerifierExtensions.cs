using GhostServiceBuster.Default;
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
                .RegisterServiceProviderServiceInfoExtractor()
                .RegisterServiceProviderUsageRootServicesFilter()
                .RegisterMicrosoftAndSystemNamespacesAllServicesFilter();
    }

    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForServiceCollection(
        this IServiceUsageVerifierWithCachedServicesMutable serviceUsageVerifier) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)
        ((IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier).ForServiceCollection();
}