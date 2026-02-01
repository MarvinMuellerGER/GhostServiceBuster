using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.MS.Filter;

file sealed class MicrosoftAndSystemNamespacesFilter : IServiceInfoFilter
{
    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s =>
            s.ServiceType.Namespace?.StartsWith(nameof(Microsoft)) is false &&
            s.ServiceType.Namespace?.StartsWith(nameof(System)) is false);
}

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifierWithCachedFiltersMutable RegisterMicrosoftAndSystemNamespacesAllServicesFilter(
        this IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterAllServicesFilter<MicrosoftAndSystemNamespacesFilter>();

    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        RegisterMicrosoftAndSystemNamespacesAllServicesFilter(
            this IServiceUsageVerifierWithCachedServicesMutable serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterAllServicesFilter<MicrosoftAndSystemNamespacesFilter>();
}