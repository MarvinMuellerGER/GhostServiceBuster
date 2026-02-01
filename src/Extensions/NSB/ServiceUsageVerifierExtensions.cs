using GhostServiceBuster.MS;
using GhostServiceBuster.NServiceBus.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.NServiceBus;

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForNServiceBus(
        this IServiceUsageVerifierWithCachedServicesMutable serviceUsageVerifier) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)
        ((IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier).ForNServiceBus();

    extension(IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier)
    {
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForNServiceBusUnsafe(
            IServiceProvider services) =>
            serviceUsageVerifier.ForServiceProviderUnsafe(services)
                .RegisterNServiceBusMessageHandlerRootServicesFilter();

        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForNServiceBus(
            IServiceCollection services) =>
            serviceUsageVerifier.ForServiceCollection(services).RegisterNServiceBusMessageHandlerRootServicesFilter();

        public IServiceUsageVerifierWithCachedFiltersMutable ForNServiceBus() =>
            serviceUsageVerifier.ForServiceCollection().RegisterNServiceBusMessageHandlerRootServicesFilter();
    }
}