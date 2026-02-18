using GhostServiceBuster.Default;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS;

/// <summary>
/// Provides extensions for configuring verifiers with Microsoft DI services.
/// </summary>
public static class ServiceUsageVerifierExtensions
{
    extension(IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier)
    {
        /// <summary>
        /// Configures the verifier for a service provider without safety checks.
        /// </summary>
        /// <param name="services">The service provider.</param>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForServiceProviderUnsafe(
            IServiceProvider services) =>
            serviceUsageVerifier.ForServiceCollection()
                .LazyRegisterAllServices(() => services);

        /// <summary>
        /// Configures the verifier for a service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForServiceCollection(
            IServiceCollection services) =>
            serviceUsageVerifier.ForServiceCollection()
                .LazyRegisterAllServices(() => services);

        /// <summary>
        /// Configures the verifier with default Microsoft DI extractors and filters.
        /// </summary>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedFiltersMutable ForServiceCollection() =>
            serviceUsageVerifier.Default()
                .RegisterServiceCollectionServiceInfoExtractor()
                .RegisterServiceProviderServiceInfoExtractor()
                .RegisterServiceProviderUsageRootServicesFilter()
                .RegisterMicrosoftAndSystemNamespacesAllServicesFilter();
    }

    /// <summary>
    /// Configures the verifier for a service collection from a cached services verifier.
    /// </summary>
    /// <param name="serviceUsageVerifier">The verifier to configure.</param>
    /// <returns>The configured verifier.</returns>
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForServiceCollection(
        this IServiceUsageVerifierWithCachedServicesMutable serviceUsageVerifier) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)
        ((IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier).ForServiceCollection();
}
