using GhostServiceBuster.MS;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.NServiceBus;

/// <summary>
/// Provides extensions for configuring verifiers with NServiceBus services.
/// </summary>
public static class ServiceUsageVerifierExtensions
{
    /// <summary>
    /// Configures the verifier for NServiceBus from a cached services verifier.
    /// </summary>
    /// <param name="serviceUsageVerifier">The verifier to configure.</param>
    /// <returns>The configured verifier.</returns>
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForNServiceBus(
        this IServiceUsageVerifierWithCachedServicesMutable serviceUsageVerifier) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)
        ((IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier).ForNServiceBus();

    extension(IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier)
    {
        /// <summary>
        /// Configures the verifier for NServiceBus without safety checks.
        /// </summary>
        /// <param name="services">The service provider.</param>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForNServiceBusUnsafe(
            IServiceProvider services) =>
            serviceUsageVerifier.ForServiceProviderUnsafe(services)
                .RegisterNServiceBusMessageHandlerRootServicesFilter();

        /// <summary>
        /// Configures the verifier for NServiceBus using the provided service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForNServiceBus(
            IServiceCollection services) =>
            serviceUsageVerifier.ForServiceCollection(services).RegisterNServiceBusMessageHandlerRootServicesFilter();

        /// <summary>
        /// Configures the verifier for NServiceBus using default registration.
        /// </summary>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedFiltersMutable ForNServiceBus() =>
            serviceUsageVerifier.ForServiceCollection().RegisterNServiceBusMessageHandlerRootServicesFilter();
    }
}
